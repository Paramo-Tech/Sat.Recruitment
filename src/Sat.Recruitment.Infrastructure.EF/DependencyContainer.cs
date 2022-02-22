using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Infrastructure.EF.EFSpecifics;
using Sat.Recruitment.Infrastructure.EF.Repositories;

namespace Sat.Recruitment.Infrastructure.EF
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureEFDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<RecruitmentDbContext>(options => options.UseInMemoryDatabase("InMemDb"));

            // Repository implementations
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
