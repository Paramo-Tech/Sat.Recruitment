using System;

namespace Sat.Recruitment.Domain.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException() : base() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception inner) : base(message, inner) { }
    }
}
