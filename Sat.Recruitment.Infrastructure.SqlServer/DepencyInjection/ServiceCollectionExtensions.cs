using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Sat.Recruitment.Infrastructure.SqlServer.DepencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all required services and classes to operate with Entity Framework Core and MS Sql Server.
        /// </summary>
        /// <param name="services">Instance of the ServiceCollection where the services will registered.</param>
        /// <param name="connectionString">Connection string of the configured database.</param>
        public static void RegisterSqlServerInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SatRecruitmentDbContext>(options =>
                options.UseSqlServer(
                connectionString,
                assembly => assembly.MigrationsAssembly(typeof(SatRecruitmentDbContext).Assembly.FullName)));

            services.AddScoped<IUserRepository, SqlServer.UserRepository>();
        }
    }
}
