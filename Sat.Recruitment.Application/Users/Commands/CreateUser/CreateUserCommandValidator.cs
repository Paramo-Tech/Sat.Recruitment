using FluentValidation;
using Sat.Recruitment.Application.Common.Interfaces;

namespace Sat.Recruitment.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.Name)
            .NotNull().WithMessage("The name is required");

        RuleFor(v => v.Email)
            .NotNull().WithMessage("The email is required");

        RuleFor(v => v.Address)
            .NotNull().WithMessage("The address is required");

        RuleFor(v => v.Phone)
            .NotNull().WithMessage("The phone is required"); 

        RuleFor(v => v.Money).Must(IsDecimal).WithMessage("The money is not a decimal number");

        RuleFor(v => v.Name).Must(NotBeDuplicate).WithMessage("The user is duplicated");
    }

    private bool IsDecimal(string money)
    {
        return decimal.TryParse(money, out _);
    }

    private bool NotBeDuplicate(CreateUserCommand createUserCommand, string value)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == createUserCommand.Email || x.Phone == createUserCommand.Phone) ??
                   _context.Users.FirstOrDefault(x => x.Name == createUserCommand.Name && x.Address == createUserCommand.Address);

        return user is null;
    }
}