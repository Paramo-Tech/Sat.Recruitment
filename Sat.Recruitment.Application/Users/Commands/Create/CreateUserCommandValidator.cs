using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Common.Interfaces;

namespace Sat.Recruitment.Application.Users.Commands.Create
{

    public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty.");
            RuleFor(x => x)
               .MustAsync(BeUnique)
               .WithMessage("User is duplicated");
        }
        private async Task<bool> BeUnique(CreateUserCommand command, CancellationToken cancellationToken)
        {

            var foundDuplicated =  await _context.Users.AnyAsync(x => (
                        x.Email == command.Email || x.Phone == command.Phone)
                        || (x.Name == command.Name && x.Address == command.Address),
                         cancellationToken);

            return !foundDuplicated;
        }
    }
}
