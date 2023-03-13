using System;
namespace Sat.Recruitment.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public DomainException()
        {
        }

        public abstract override string Message { get; }

        /// <summary>
        /// This is an error string that could be localized in the front end.
        /// </summary>
        public abstract string ErrorCode { get; }
    }
}

