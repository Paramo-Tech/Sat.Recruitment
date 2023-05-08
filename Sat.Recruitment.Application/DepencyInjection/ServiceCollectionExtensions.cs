using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Automapper;
using Sat.Recruitment.Application.Command;
using Sat.Recruitment.Application.Services.GifCalculator.Factory;

namespace Sat.Recruitment.Application.DepencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all required services and classes from the application layer.
        /// </summary>
        /// <param name="services">Instance of the ServiceCollection where the services will registered.</param>
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));
            services.AddAutoMapper(typeof(UserProfile));
            services.AddTransient<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddTransient<IUserGifCalculatorFactory, UserGifCalculatorFactory>();
        }
    }
}
