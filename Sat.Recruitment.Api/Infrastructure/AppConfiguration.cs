using Sat.Recruitment.BusinessLogic.ExternalServices;

namespace Sat.Recruitment.Api.Infrastructure
{
    public class AppConfiguration : IAppConfiguration
    {
        public string FilePath { get; set; }
    }
}