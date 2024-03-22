namespace StudentApp.API.Extentions
{
    public static class CORSExtension
    {
        public static IServiceCollection CustomCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MyCustomCorsPolicy", c =>
                {
                    c.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            return services;
        }
    }
}
