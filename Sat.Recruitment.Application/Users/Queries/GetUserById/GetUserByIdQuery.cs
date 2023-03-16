using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Dto;

namespace Sat.Recruitment.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequestWrapper<UserDto>
    {
        public int UserId { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandlerWrapper<GetUserByIdQuery, UserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(x => x.Id == request.UserId)
                .ProjectToType<UserDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            return user != null ? ServiceResult.Success(user) : ServiceResult.Failed<UserDto>(ServiceError.NotFound);
        }
    }
}
