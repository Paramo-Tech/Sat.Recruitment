using Application.Contracts;
using Application.Contracts.Validators;
using Application.Factories;
using Application.Services;
using Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserCreationValidator, UserCreationValidator>();

            services.AddGiftFactory();

        }

        static void AddGiftFactory(this IServiceCollection services)
        {
            services.AddTransient<IGiftService, NormalGiftService>();
            services.AddTransient<IGiftService, PremiumGiftService>();
            services.AddTransient<IGiftService, SuperGiftService>();

            services.AddTransient<Func<IEnumerable<IGiftService>>>
                (x => () => x.GetService<IEnumerable<IGiftService>>()!);

            services.AddTransient<IGiftFactory, GiftFactory>();
        }
    }
}