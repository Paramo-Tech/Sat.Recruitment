using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.BusinessLogic;
using Sat.Recruitment.Application.Interfaces.Repositories;
using Sat.Recruitment.Application.Interfaces.Services;
using Sat.Recruitment.Infrastructure.BusinessLogic;
using Sat.Recruitment.Infrastructure.Repositories;
using Sat.Recruitment.Infrastructure.Services;

namespace Sat.Recruitment.Infrastructure
{
    public static class InfrastructureDependencyRegistration
    {
        public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IUserBL, UserBL>();

            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
