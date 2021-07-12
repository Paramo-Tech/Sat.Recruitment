using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;


namespace Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User dbUser = await _userRepository.FindByEmailAsync(request.Email, cancellationToken);
            if (dbUser != null)
                throw new DuplicateUserException();

            User user = _mapper.Map<User>(request);
            user = await _userRepository.AddAsync(user);
            UserDto result = _mapper.Map<UserDto>(user);
            return result;
        }
    }
}
