using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Sat.Recruitment.Domain.Entities.UserAggregate;
using Sat.Recruitment.Application.Commands.CreateUser;

namespace Sat.Recruitment.Application.Extensions
{
    public static class ServiceExtension
	{
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidator<User>, CreateUserCommandValidator>();

            return services;
        }
    }
}

