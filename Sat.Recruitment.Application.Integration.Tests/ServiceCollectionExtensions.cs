using Microsoft.Extensions.DependencyInjection;

namespace Sat.Recruitment.Application.Integration.Test
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Remove<TService>(this IServiceCollection services)
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