using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Entities.Exceptions
{
    public class DuplicatedUserException : Exception
    {
        public DuplicatedUserException() : base("The user is duplicated")
        {
        }

        public DuplicatedUserException(string message) : base(message)
        {
        }

        public DuplicatedUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
