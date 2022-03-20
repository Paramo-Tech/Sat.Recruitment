using MediatR;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Users.Queries
{
    public class GetAllActiveUsersHandler : IRequestHandler<GetAllActiveUsersQuery, List<User>>
    {
        private readonly UsersContext _context;
        public GetAllActiveUsersHandler(UsersContext context) => _context = context;
        public async Task<List<User>> Handle(GetAllActiveUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking().Where(x => x.IsActive).ToListAsync(cancellationToken);
        }
    }
}
