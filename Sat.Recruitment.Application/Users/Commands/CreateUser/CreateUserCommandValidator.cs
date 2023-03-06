using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Base.Interfaces;
using System.Threading;

namespace Sat.Recruitment.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IApplicationDbContext _context;
    
    public CreateUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(u => u.Name)
            .NotEmpty();

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Address)
            .NotEmpty();

        RuleFor(u => u.Phone)
            .NotEmpty();

        RuleFor(u => u)
            .Must(u => !IsDuplicate(u))
            .WithMessage("User is duplicated");
    }

    private bool IsDuplicate(CreateUserCommand user)
    {                
        if (_context.Users.Any(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase) ||
                                    u.Phone.Equals(user.Phone, StringComparison.OrdinalIgnoreCase)))
            return true;

        if (_context.Users.Any(u => u.Name.Equals(user.Name, StringComparison.OrdinalIgnoreCase) &&
                                    u.Address.Equals(user.Address, StringComparison.OrdinalIgnoreCase)))
            return true;

        return false;
    }
}
