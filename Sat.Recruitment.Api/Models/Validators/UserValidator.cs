using FluentValidation;
using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Repository.Interfaces;
using System;
using System.Net.Mail;

namespace Sat.Recruitment.Api.Models.Validators
{

    public class UserValidator : AbstractValidator<UserDTO>
    {
        private readonly IUserRepository _repository;

        public UserValidator(IUserRepository repository)
        {

            this._repository = repository;
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Phone)
                .NotNull().NotEmpty().WithMessage("Phone is required");
            RuleFor(x => x.Email)
                .NotNull().NotEmpty().WithMessage("Email is required")
                .Custom((email, context) =>
                {
                    email = NormalizeEmail(email);
                }); 
            RuleFor(x=>x.Email)
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Address)
                .NotNull().NotEmpty().WithMessage("Address is required");

            RuleFor(x => x.Email)
                .Must(x => !_repository.Any(u => u.Email.Equals(x)))
                .WithMessage("User is duplicated");
            RuleFor(x => x.Phone)
                .Must(x => !_repository.Any(u => u.Phone.Equals(x)))
                .WithMessage("User is duplicated");
            RuleFor(x => x)
                .Custom((newUser, context) =>
                    {
                        if (_repository.Any(user => user.Name == newUser.Name && user.Address == newUser.Address))
                        {
                            context.AddFailure("Name and Address", "User is duplicated");
                        }
                    });
        }

        private string NormalizeEmail(string email)
        {

            MailAddress.TryCreate(email, out MailAddress result);
            if (result != null)
            {
                var username = result.User;
                username = username.Replace(".", "");
                int atIndex = username.IndexOf("+", StringComparison.Ordinal);
                username = atIndex < 0 ? username : username.Remove(atIndex);
                return string.Join("@", new string[] {
                    username,
                    result.Host});
            }

            return email;
        }
    }
}
