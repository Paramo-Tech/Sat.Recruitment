using Application.Contracts;
using Application.Contracts.Repositories;
using Application.Extensions;
using Application.Models;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGiftFactory _factory;
        private readonly IMapper _mapper;
        private readonly UserCreationValidator _userCreationValidator;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IGiftFactory factory, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _userCreationValidator = new UserCreationValidator();
            _factory = factory;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result> Create(UserCreationDto userCreationDto)
        {
            _logger.LogInformation("UserService.Create. Start, email: {email}", userCreationDto.Email);
            var result = _userCreationValidator.Validate(userCreationDto);

            if (!result.IsValid)
            {
                var errors = string.Join(" ", result.Errors.Select(x => x.ErrorMessage));
                _logger.LogError("UserService.Create. Validation error, errorMessage: {errors}", errors);
                return Result.Failure(errors);
            }

            var user = _mapper.Map<User>(userCreationDto);

            var users = await _userRepository.GetAllAsync();

            try
            {
                var isDuplicated = users.Any(x => x.Email == user.Email || x.Phone == user.Phone || (x.Name == user.Name && x.Address == user.Address));

                if (isDuplicated)
                {
                    _logger.LogError("UserService.Create. The user is duplicated, email: {email}", userCreationDto.Email);

                    return Result.Failure("The user is duplicated");
                }

                var giftService = _factory.Create(user.UserType);

                user.Money = giftService.GetDiscount(user.Money);
                user.Email = user.Email.NormalizeEmail();

                await _userRepository.CreateAsync(user);

                _logger.LogInformation("UserService.Create. The user was Created, email: {email}", userCreationDto.Email);

                return Result.Success("User Created");

            }
            catch (Exception ex)
            {
                _logger.LogError("UserService.Create. Error while creating an user email: {email} and exception message", userCreationDto.Email, ex.Message);

                return Result.Failure("The user is duplicated");
            }
        }
    }
}