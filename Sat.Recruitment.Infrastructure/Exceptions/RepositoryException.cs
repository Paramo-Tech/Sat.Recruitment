using System;

namespace Sat.Recruitment.Infrastructure.Exceptions
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException() : base() { }

        public RepositoryException(string message) : base(message) { }

        public RepositoryException(string message, Exception inner) : base(message, inner) { }
    }
}
