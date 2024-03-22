using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApp.API.Extentions;
using StudentApp.API.Middleware;
using StudentApp.Core.Infra;
using StudentApp.Infrastructure.Common;
using StudentApp.Infrastructure.Data;
using System.Reflection;

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
builder.Services.AddSwaggerGen();

//efcore
builder.Services.AddDbContext<StudentDbContext>(ops =>
{
    ops.UseSqlServer(builder.Configuration["ConnectionStrings:StudentConnectionString"]).EnableSensitiveDataLogging();
});

//cors
builder.Services.CustomCORS();

//custom
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<GlobalExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyCustomCorsPolicy");
app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
