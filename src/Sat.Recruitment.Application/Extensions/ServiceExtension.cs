using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Domain.Interfaces.Data.Repositories;

namespace Sat.Recruitment.Application.Extensions
{
    public static class ServiceExtension
	{
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());            

            return services;
        }
    }
}

