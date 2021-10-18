using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Data.Definitions;
using Sat.Recruitment.Data.Implementations;

namespace Sat.Recruitment.Data
{
    public static class DataExtension
    {
        public static IServiceCollection RegisterData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
