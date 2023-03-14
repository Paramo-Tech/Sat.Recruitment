using System;

namespace Sat.Recruitment.Core.ResponsesExceptions
{
    public class NotFoundException : Exception
    {
        public string ErrorMessage { get; set; }
    }
}
