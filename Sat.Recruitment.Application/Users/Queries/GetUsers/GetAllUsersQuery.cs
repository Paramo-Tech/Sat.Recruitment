using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Dto;

namespace Sat.Recruitment.Application.Users.Queries.GetUsers
{
    public class GetAllUsersQuery : IRequestWrapper<List<UserDto>>
    {

    }

    public class GetCitiesQueryHandler : IRequestHandlerWrapper<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<UserDto> list = await _context.Users
                .ProjectToType<UserDto>(_mapper.Config)
                .ToListAsync(cancellationToken);
            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<UserDto>>(ServiceError.NotFound);
        }
    }
}
