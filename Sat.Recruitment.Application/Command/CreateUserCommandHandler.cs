using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Dto.User;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Application.Services.GifCalculator.Factory;
using Sat.Recruitment.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Sat.Recruitment.Application.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserCommand> _commandValidator;
        private readonly IUserGifCalculatorFactory _userGifCalculator;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
            ILogger<CreateUserCommandHandler> logger,
            IUserRepository userRepository,
            IValidator<CreateUserCommand> commandValidator,
            IUserGifCalculatorFactory userGifCalculator,
            IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _commandValidator = commandValidator;
            _userGifCalculator = userGifCalculator;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Processing new user. Payload: {0}", request);            
            _commandValidator.ValidateAndThrow(request);

            var newUser = request.ToUser();
            var calculator = _userGifCalculator.CreateCalculator(newUser.UserType);
            newUser.Money = calculator.Calculate(newUser.Money);

            var users = _userRepository.GetAll();
            var isRepeated = users.Where(x => x.Email == newUser.Email || x.Phone == newUser.Phone || (x.Name == newUser.Name && x.Address == newUser.Address))
                                  .Any();

            if (!isRepeated)
            {
                var user = await _userRepository.AddAsync(newUser);

                _logger.LogDebug("New user inserted succesfully.");
                return _mapper.Map<UserDto>(user);
            }
            else
            {
                _logger.LogError("New user request failed, repeated user: {userName}.", request);
                throw new RepeatedUserException(request.Name);
            }
        }
    }
}
