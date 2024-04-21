using AuthApp.API.Extensions;
using AuthApp.API.MIddlewares;
using AuthApp.Core.Infra;
using AuthApp.Core.Services;
using AuthApp.Infrastructure.Common;
using AuthApp.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//global exception middleware
builder.Services.AddScoped<GlobalExceptionMiddleware>();
//cors
builder.Services.CustomCORS();

//mediatr
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("AuthApp.Core"));
});

//fluent validator
builder.Services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.Load("AuthApp.Core"));
builder.Services.AddFluentValidationAutoValidation();

//mapster
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(AppDomain.CurrentDomain.Load("AuthApp.Core"));
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

//ef core
builder.Services.AddDbContext<AuthAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnectionString"]).EnableSensitiveDataLogging();
});

//jwt authentication
builder.Services.JTWAuthConfig(builder.Configuration);
builder.Services.AddAuthorization();

//custom
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.RepositoryConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyCustomCORSPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
