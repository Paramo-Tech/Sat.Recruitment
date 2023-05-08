using System;

namespace Sat.Recruitment.Application.Dto
{
    [Obsolete("Deprecated. Maintained temporarily for backwards compatibility.")]
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
