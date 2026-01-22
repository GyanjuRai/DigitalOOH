using DigitalOOH.API.Interfaces.Application.Ads;
using DigitalOOH.API.Interfaces.Application.Campaigns;
using DigitalOOH.API.Interfaces.Application.Screens;
using DigitalOOH.API.Interfaces.Shared.Account;
using DigitalOOH.API.Interfaces.Shared.Auth;
using DigitalOOH.API.Interfaces.Shared.Media;
using DigitalOOH.API.Services.Application.Ads;
using DigitalOOH.API.Services.Application.Campaigns;
using DigitalOOH.API.Services.Application.Screens;
using DigitalOOH.API.Services.Shared.Account;
using DigitalOOH.API.Services.Shared.Auth;
using DigitalOOH.API.Services.Shared.Media;

namespace DigitalOOH.API.Middlewares
{
    public static class DIContainerMiddleware
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>()
                .AddScoped<IAccountService, AcountService>()
                .AddTransient<IMediaService, MediaService>();

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IScreeensService, ScreensService>()
                .AddTransient<IAdsService, AdsService>()
                .AddTransient<ICampaignsService, CampaignsService>();

            return services;
        }

    }
}
