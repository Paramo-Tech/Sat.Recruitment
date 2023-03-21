using System;
using FluentValidation;

namespace Sat.Recruitment.Application.Commands.CreateUser
{
	public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
		public CreateUserCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
			RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
			RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
			RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required");
		}
	}
}

