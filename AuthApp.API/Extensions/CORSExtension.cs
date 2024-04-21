namespace AuthApp.API.Extensions
{
    public static class CORSExtension
    {
        public static IServiceCollection CustomCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MyCustomCORSPolicy", c =>
                {
                    c.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });
            return services;
        }
    }
}
