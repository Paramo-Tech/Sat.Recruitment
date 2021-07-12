using Application.Dtos;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserByEmailQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            User dbUser = await _userRepository.FindByEmailAsync(request.Email, cancellationToken);
            if (dbUser == null)
                return null;

            UserDto result = _mapper.Map<UserDto>(dbUser);
            return result;
        }
    }
}
