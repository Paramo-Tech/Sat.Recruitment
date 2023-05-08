using System;

namespace Sat.Recruitment.Application.Exceptions
{
    [Serializable]
    public class RepeatedUserException : Exception
    {
        public const string message = "The user is repeated.";

        public RepeatedUserException() : base() { }

        public RepeatedUserException(string userName) : base($"{message}. Name: {userName}") 
        { 
        }

        public RepeatedUserException(string userName, Exception inner) : base($"{message}. Name: {userName}", inner) { }
    }
}
