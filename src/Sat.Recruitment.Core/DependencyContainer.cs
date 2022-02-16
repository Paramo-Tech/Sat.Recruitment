using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType;
using Sat.Recruitment.Core.Abstractions.BusinessFeatures.NormalizeEmail;
using Sat.Recruitment.Core.BusinessRules.Features.GiftByUserType;
using Sat.Recruitment.Core.BusinessRules.Features.NormalizeEmail;

namespace Sat.Recruitment.Core
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Gift by UserType Mediator
            services.AddSingleton<IGiftByUserTypeMediator, GiftByUserTypeMediator>();

            // Add UserType Gift Strategies
            services.AddSingleton<INormalUserGiftStrategy, NormalUserGiftStrategy>();
            services.AddSingleton<IPremiumUserGiftTrategy, PremiumUserGiftTrategy>();
            services.AddSingleton<ISuperUserGiftStrategy, SuperUserGiftStrategy>();

            // Add NormalizeEmail functionality
            services.AddSingleton<INormalizeEmail, NormalizeEmail>();

            return services;
        }
    }
}
