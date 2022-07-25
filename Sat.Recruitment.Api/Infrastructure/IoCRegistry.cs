using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.BusinessLogic.ExternalServices;
using Sat.Recruitment.BusinessLogic.Services;
using Sat.Recruitment.DataAccess.Helpers;
using Sat.Recruitment.DataAccess.Repository;

namespace Sat.Recruitment.Api.Infrastructure
{
    public class IoCRegistry
    {
        public static string[] CorsAllowedOrigins { get; private set; }

        public static void Register(IServiceCollection services, IConfiguration configuration) 
        {
            #region Parameters
            CorsAllowedOrigins = configuration.GetSection("CorsAllowedOrigins").Get<string[]>();
            IAppConfiguration appConfiguration = configuration.GetSection("AppConfig").Get<AppConfiguration>();
            services.AddSingleton(appConfiguration);
            #endregion

            #region Helpers
            services.AddSingleton<IEncryptDecrypt, EncryptDecrypt>(); 
            #endregion

            #region Services
            services.AddScoped<IUserSvc, UserSvc>();
            services.AddScoped<IDataAccess, DataAccess.DataAccess>();
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>(); 
            #endregion
        }
    }
}