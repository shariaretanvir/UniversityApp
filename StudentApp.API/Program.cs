using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using StudentApp.API.Extentions;
using StudentApp.API.Middleware;
using StudentApp.Core.Infra;
using StudentApp.Infrastructure.Common;
using StudentApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomValidationFilters>();
});

//mediatr
builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("StudentApp.Core"));
});

//fluent validator
builder.Services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.Load("StudentApp.Core"));
builder.Services.AddFluentValidationAutoValidation();

//mapster
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(AppDomain.CurrentDomain.Load("StudentApp.Core"));
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//efcore
builder.Services.AddDbContext<StudentDbContext>(ops =>
{
    ops.UseSqlServer(builder.Configuration["ConnectionStrings:StudentConnectionString"]).EnableSensitiveDataLogging();
}, ServiceLifetime.Scoped);

//inject repository
builder.Services.Scan(scan => scan.FromAssemblies(AppDomain.CurrentDomain.Load("StudentApp.Infrastructure"))
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
    .AsImplementedInterfaces()
    .WithTransientLifetime());

//cors
builder.Services.CustomCORS();

//jwtauth
builder.Services.JWTAuthConfig(builder.Configuration);
builder.Services.AddAuthorization();

//swagger
builder.Services.SwaggerConfig();

//custom
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<GlobalExceptionMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(ui => ui.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth Api"));
}
app.UseCors("MyCustomCorsPolicy");
app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
