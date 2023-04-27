using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Infrastructure.Persistence;
using Sat.Recruitment.Infrastructure.Services;

namespace Sat.Recruitment.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<IDbContext, SatDbContext>(options => options.UseInMemoryDatabase("DFrameworkDb"));
        }
        else
        {
            services.AddDbContext<IDbContext, SatDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!, builder => builder.MigrationsAssembly(typeof(SatDbContext).Assembly.FullName)));
        }

        services.AddScoped<SatDbContextInitializer>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddMemoryCache();
        return services;
    }
}