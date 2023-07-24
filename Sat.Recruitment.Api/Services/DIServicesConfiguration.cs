using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.Services.Service;

namespace Sat.Recruitment.Api.Services
{
    public static class DIServicesConfiguration
    {
        public static IServiceCollection ConfigureServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUser, UserService_>();
            services.AddScoped<IReadUserFile, ReadUserFileService>();

          
            return services;
        }

     
    }
}
