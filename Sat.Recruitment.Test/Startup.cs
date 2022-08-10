using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace Sat.Recruitment.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IQueriesUserService, QueriesUserService>();
            services.AddMediatR(Assembly.Load("Sat.Recruitment.Api"));
            services.AddTransient<UsersController>();
        }
    }
}
