using System;

namespace Sat.Recruitment.Core.ResponsesExceptions
{
    public class BadRequestException : Exception
    {
        public string ErrorMessage { get; set; }
    }
}
