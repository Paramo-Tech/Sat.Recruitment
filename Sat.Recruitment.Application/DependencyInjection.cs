using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Services;
using Sat.Recruitment.Application.Services.Interfaces;

namespace Sat.Recruitment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
