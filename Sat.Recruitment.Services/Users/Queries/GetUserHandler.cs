using MediatR;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Users.Queries
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly UsersContext _context;
        public GetUserHandler(UsersContext context) => _context = context;

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
