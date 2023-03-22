using System;
using FluentValidation;
using Sat.Recruitment.Domain.Interfaces.Data.Repositories;

namespace Sat.Recruitment.Application.Commands.CreateUser
{
	public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
		private readonly IUserRepository _userRepository;

		public CreateUserCommandValidator(IUserRepository userRepository)
		{
			_userRepository = userRepository;

			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage("Name is required");

			RuleFor(x => x.Address)
				.NotEmpty()
				.WithMessage("Address is required");

			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Email is required");

			RuleFor(x => x.Phone)
				.NotEmpty()
				.WithMessage("Phone is required");

			RuleFor(x => x)
				.Must(x => !_userRepository
				.IsDublicateUser(x.Name, x.Email, x.Address, x.Phone).Result)
				.WithMessage("Duplicated user");
		}
	}
}

