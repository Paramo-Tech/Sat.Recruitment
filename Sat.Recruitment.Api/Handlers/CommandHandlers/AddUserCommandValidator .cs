using FluentValidation;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Requests.Commands;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Handlers.CommandHandlers
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(obj => obj.name).NotEmpty().WithMessage("The name required");
            RuleFor(obj => obj.email).NotEmpty().WithMessage("The email required");
            RuleFor(obj => obj.address).NotEmpty().WithMessage("The address required");
            RuleFor(obj => obj.phone).NotEmpty().WithMessage("The phone required");
            RuleFor(obj => obj.email).Must(Services.Services.IsValidEmail).WithMessage("Email is not valid");
            RuleFor(obj => obj.userType).Must(IsValidUserType).WithMessage("User Type is not valid");
        }

        private bool IsValidUserType(string userType)
        {
            return Enum.TryParse(typeof(UserType), userType, out object userTypeResult);
        }
    }
}