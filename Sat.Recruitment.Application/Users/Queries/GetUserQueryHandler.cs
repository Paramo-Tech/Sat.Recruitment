using MediatR;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Base.Interfaces;

namespace Sat.Recruitment.Application.Users.Queries;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IList<GetUserDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUserQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var usersDto = new List<GetUserDto>();
        await _context.Users.ForEachAsync(u => usersDto.Add(u.MapTo()), cancellationToken: cancellationToken);
        return usersDto;
    }
}
