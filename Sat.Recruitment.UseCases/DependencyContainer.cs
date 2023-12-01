using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.UseCases.CreateUser;
using Sat.Recruitment.UseCases.Services.FileReader;
using Sat.Recruitment.UseCases.Services.UserBonus;
using Sat.Recruitment.UseCasesAbstractions;
using System.Drawing;

namespace Sat.Recruitment.UseCases
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
        {
            services.AddTransient<IPostUserInputPort, UserIterator>();
            services.AddTransient<IFileReaderService, FileReaderService>();
            services.AddTransient<IUserBonusService, UserBonusService>();
            services.AddTransient<IUserTypeBonusFactory, UserTypeBonusFactory>();
            services.AddTransient<NormalUserType>();
            services.AddTransient<PremiumUserType>();
            services.AddTransient<SuperUserType>();
            return services;

        }
    }
}