using System.Diagnostics.CodeAnalysis;

namespace Sat.Recruitment.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class Result<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
