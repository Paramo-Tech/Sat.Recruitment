using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Sat.Recruitment.Application.Common.Behaviors;
using Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;

namespace Sat.Recruitment.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddSingleton<IUserGifService, UserGifService>();
        services.AddSingleton<IUserGifStrategy, NormalUserGifStrategy>();
        services.AddSingleton<IUserGifStrategy, PremiumUserGifStrategy>();
        services.AddSingleton<IUserGifStrategy, SuperUserGifStrategy>();

        return services;
    }
}