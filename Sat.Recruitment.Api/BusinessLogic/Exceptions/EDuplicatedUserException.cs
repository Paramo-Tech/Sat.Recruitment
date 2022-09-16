using System;

namespace Sat.Recruitment.Api.BusinessLogic.Exceptions
{
    public class EDuplicatedUserException : EVisibleException
    {
        public EDuplicatedUserException(string msg) : base($"Duplicated User ({msg})") { }
    }
}
