using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Common.Interfaces;

namespace Sat.Recruitment.Application.Users.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CreateUserCommandValidator(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            RuleFor(v => v)
                .MustAsync(MustBeUnique)
                .WithMessage("The user is duplicated");                
            RuleFor(v => v.Email)
                .NotNull().WithMessage("The email is required")
                .EmailAddress().WithMessage("A valid email address is required");
            RuleFor(v => v.Name).NotNull().WithMessage("The name is required");
            RuleFor(v => v.Address).NotNull().WithMessage("The address is required");
            RuleFor(v => v.Phone).NotNull().WithMessage("The phone is required");
        }

        private async Task<bool> MustBeUnique(CreateUserCommand command, CancellationToken cancellationToken)
        {
            return !await _applicationDbContext!.Users.AnyAsync(x =>
                x.Email.Equals(command.Email) ||
                x.Phone.Equals(command.Phone) ||
                (x.Name.Equals(command.Name) && x.Address.Equals(command.Address)), cancellationToken: cancellationToken);
        }
    }
}
