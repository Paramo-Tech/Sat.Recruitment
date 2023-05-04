using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Rec.Core.Infrastructure;
using Sat.Rec.Core.Repository;
using Sat.Rec.Core.Repository.Interfaces;

namespace Sat.Rec.Core
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbUsersContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("GeneralConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<IGIFUserTypeRepository, GIFUserTypeRepository>();

            return services;
        }
    }
}