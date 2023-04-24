using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Exceptions
{
    public class CreateUserException<T>: Exception
    {
        public CreateUserException(string error) : 
            base($"Error validating createUser {nameof(T)}. Error: '{error}'") 
        { }
    }
}
