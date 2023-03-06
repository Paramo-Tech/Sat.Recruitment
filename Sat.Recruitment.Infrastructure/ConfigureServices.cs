using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Base.Interfaces;
using Sat.Recruitment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Sat.Recruitment.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("UsersDb"));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        return services;
    }
}
