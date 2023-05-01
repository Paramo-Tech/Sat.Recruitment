using MediatR;
using Sat.Recruitment.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<IEnumerable<User>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly IApplicationDbContext _context;

    public GetUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.AsNoTracking().ToListAsync(cancellationToken);
    }
}