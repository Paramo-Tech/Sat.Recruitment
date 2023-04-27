using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Users.Models;

namespace Sat.Recruitment.Application.Users.Queries
{
    public class GetUsersQuery : IRequest<IList<UserViewModel>> { }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IList<UserViewModel>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IList<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users
                .AsNoTracking()
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }
    }
}
