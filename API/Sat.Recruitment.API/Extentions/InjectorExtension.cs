using Application.Services;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Sat.Recruitment.API.Extentions
{
    [ExcludeFromCodeCoverage]
    internal static class InjectorExtension
    {
        /// <summary>
        /// Método que agrega la dependencias para ser usas en IoC
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        internal static void AddDependencys(this IServiceCollection services, IConfiguration configuration)
        {
            #region Domain
            services.AddScoped<IUserService, UserService>();
            #endregion
            #region Infraestructure
            services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
