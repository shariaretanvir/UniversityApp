using Microsoft.OpenApi.Models;

namespace StudentApp.API.Extentions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection SwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                });
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Auth Api",
                    Version = "v1",
                    Description = "Auth Api",
                    Contact = new OpenApiContact
                    {
                        Name = "Auth"
                    }
                });

                // Add JWT bearer token authorization to Swagger
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

            });
            return services;
        }
    }
}
