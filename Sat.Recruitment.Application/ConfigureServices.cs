using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Common.Bahaviors;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Services;
using System.Reflection;

namespace Sat.Recruitment.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<IMoneyCalculatorService, MoneyCalculatorService>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            return services;
        }
    }
}