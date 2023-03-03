using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class CoreDIRegistrator
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR((cfg) =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
