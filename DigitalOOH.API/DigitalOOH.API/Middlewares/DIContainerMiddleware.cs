using DigitalOOH.API.Interfaces.Shared.Auth;
using DigitalOOH.API.Services.Shared.Auth;

namespace DigitalOOH.API.Middlewares
{
    public static class DIContainerMiddleware
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }

    }
}
