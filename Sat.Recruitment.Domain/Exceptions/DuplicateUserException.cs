using System;
using Sat.Recruitment.Domain.ValueObjects;

namespace Sat.Recruitment.Domain.Exceptions
{
    public sealed class DuplicateUserException : DomainException
    {
        private readonly UserName name;
        private readonly Email email;

        public DuplicateUserException(UserName name, Email email) : base()
        {
            this.name = name;
            this.email = email;
        }
        public override string Message => $"The user with name={this.name.Value} and email={this.email.Value} already exists";

        public override string ErrorCode => "mfeconfig_already_exists";

    }
}

