using System.Diagnostics.CodeAnalysis;

namespace Sat.Recruitment.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class DBSettings : IDBSettings
    {
        public string Server { get; set; }
        public string Database { get; set ; }
        public string Collection { get; set; }
    }
}
