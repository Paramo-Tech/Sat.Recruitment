using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Common.Extensions;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Common.Mappings;
using Sat.Recruitment.Application.Common.Models;
using Sat.Recruitment.Application.Users.Models;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Events;

namespace Sat.Recruitment.Application.Users.Commnads
{
    /// <summary>
    /// Contains the information required to create an User
    /// </summary>
    public class CreateUserCommand : UserViewModel, IMapFrom<UserViewModel>, IRequest<(Result, Guid)>
    {
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, (Result, Guid)>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateUserCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(Result, Guid)> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string email = request.Email.NormalizeEmail();

            if (await _dbContext.Users.AnyAsync(x => x.Email == email || x.Phone == request.Phone || (x.Name == request.Name && x.Address == request.Address)))
            {
                return (Result.Failure(new string[] { "The user is duplicated." }), Guid.Empty);
            }

            var entity = new User(request.Name, email, request.Address, request.Phone, request.UserType, request.Money, true);

            entity.AddDomainEvent(new UserCreatedEvent(entity));
            _dbContext.Users.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return (Result.Success(), entity.Id);
        }
    }
}
