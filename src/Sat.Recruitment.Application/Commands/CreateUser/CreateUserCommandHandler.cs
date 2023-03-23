using Microsoft.Extensions.Logging;
using MediatR;
using Sat.Recruitment.Domain.Interfaces.Data.Repositories;
using AutoMapper;
using Sat.Recruitment.Domain.Entities.UserAggregate;
using FluentValidation;

namespace Sat.Recruitment.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validator;


        public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger, IMapper mapper, IValidator<User> validator)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {            
            var userEntity = _mapper.Map<User>(request);
            _logger.LogDebug("UserCommand map to userEntity");

            var result = await _validator.ValidateAsync(userEntity);

            if (!result.IsValid)
            {
                _logger.LogDebug("Validations with errors");
                var errorsString = string.Empty;

                foreach (var failure in result.Errors)
                {
                    errorsString += $"Property: {failure.PropertyName}, Message: {failure.ErrorMessage} -";
                }

                throw new Exceptions.ValidationException(errorsString);
            }

            _logger.LogDebug("Validations passed");

            var id = await _userRepository.AddUserAsync(userEntity);
            _logger.LogDebug("User Created successfully");

            var response = _mapper.Map<CreateUserCommandResponse>(userEntity);
            response.Id = id;
            _logger.LogDebug("UserEntity map to UserCommandResponse");

            return response;
        }
    }
}

