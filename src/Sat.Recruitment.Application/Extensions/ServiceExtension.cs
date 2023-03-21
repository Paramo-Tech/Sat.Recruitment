using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Sat.Recruitment.Application.Extensions
{
    public static class ServiceExtension
	{
        public static IServiceCollection AddMediatRApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

