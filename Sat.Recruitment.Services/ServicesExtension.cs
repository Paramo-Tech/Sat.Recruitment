using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Data;
using Sat.Recruitment.Services.Definitions;
using Sat.Recruitment.Services.Implementations;

namespace Sat.Recruitment.Services
{
    public static class ServicesExtension
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService,UserService>();
            services.RegisterData(configuration);
            return services;
        }
    }
}
