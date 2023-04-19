using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Services;
using Sat.Recruitment.Infrastructure.Repositories;
using System;
using System.IO;

namespace Sat.Recruitment.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        //public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddDbContext<SocialMediaContext>(options =>
        //       options.UseSqlServer(configuration.GetConnectionString("SocialMedia"))
        //   );

        //    return services;
        //}

        //public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.Configure<PaginationOptions>(options => configuration.GetSection("Pagination").Bind(options));
        //    services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));

        //    return services;
        //}

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            string filePath = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            services.AddTransient<IUserRepository>(serviceProvider => new UserFileRepository(filePath));
            services.AddTransient<IUserService, UserService>();
           

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFileName)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media API", Version = "v1" });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                doc.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
