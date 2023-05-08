using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Api.Options;

namespace Sat.Recruitment.Api.Extensions
{
    public static class IConfigurationExtensions
    {
        public static PersistenceType? GetPersistenceType(this IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("PersistenceOptions");
            var persistenceType = appSettingsSection.Get<PersistenceOptions>()?.Type;

            return persistenceType;
        }
    }
}
