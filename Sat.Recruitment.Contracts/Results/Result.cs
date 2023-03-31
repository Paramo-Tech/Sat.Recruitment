using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Contracts.Results
{
    /// <summary>
    /// Result class summary
    /// </summary>
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
