namespace AuthApp.API.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection RepositoryConfig(this IServiceCollection services)
        {
            services.Scan(x => x.FromAssemblies(AppDomain.CurrentDomain.Load("AuthApp.Infrastructure"))
                .AddClasses(c => c.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return services;
        }
    }
}
