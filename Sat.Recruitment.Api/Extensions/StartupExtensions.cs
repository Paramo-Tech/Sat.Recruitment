using System;
using MongoDB.Driver;
using Sat.Recruitment.Application;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Infrastructure;
using Sat.Recruitment.Infrastructure.MongoDb;

namespace Sat.Recruitment.Api.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencies(configuration);
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddSingleton<IMongoDatabase>(options =>
            {
                var settings = configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
                var client = new MongoClient(settings?.ConnectionString);
                return client.GetDatabase(settings?.DatabaseName);
            });
            //services.AddScoped<IUserRepository, FileUserRepository>();
            services.AddScoped<IUserRepository, MongoDbUserRepository>();
            services.AddScoped<UserCreator>();

            return services;
        }
    }
}

