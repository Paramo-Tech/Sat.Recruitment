using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)//, IWebHostEnvironment environment)
        {
            var provider = configuration.GetValue("DbProvider", "SqlServer");
            var migrationAssembly = $"Sat.Recruitment.Infrastructure.{provider}";
            services.AddDbContext<ApplicationDbContext>(options => _ = provider switch
            {
                "Sqlite" => options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection_Sqlite"),
                    b =>
                    {
                        b.MigrationsAssembly(migrationAssembly);
                    })
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


            services.AddTransient<HttpClientHandler, HttpClientHandler>();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(options =>
            //    {
            //        options.SaveToken = true;
            //        options.RequireHttpsMetadata = false;
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidAudience = configuration["JWT:ValidAudience"],
            //            ValidIssuer = configuration["JWT:ValidIssuer"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            //        };
            //    })
            //    .AddIdentityServerJwt();

            return services;
        }
    }
}
