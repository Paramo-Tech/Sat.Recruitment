using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Sat.Recruitment.Application.IntegrationTests
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection Remove<TService>(this IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(TService));

            if (serviceDescriptor != null)
            {
                services.Remove(serviceDescriptor);
            }

            return services;
        }
    }
}
