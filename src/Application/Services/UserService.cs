using Application.Contracts;
using Application.Contracts.Repositories;
using Application.Extensions;
using Application.Models;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
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

        public UserService(IUserRepository userRepository, IGiftFactory factory, IMapper mapper)
        {
            _userRepository = userRepository;
            _userCreationValidator = new UserCreationValidator();
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<Result> Create(UserCreationDto userCreationDto)
        {
            var result = _userCreationValidator.Validate(userCreationDto);

            if (!result.IsValid)
            {
                var errors = string.Join(" ", result.Errors.Select(x => x.ErrorMessage));
                return Result.Failure(errors);
            }

            var user = _mapper.Map<User>(userCreationDto);            

            var users = await _userRepository.GetAllAsync();

            try
            {
                var isDuplicated = users.Any(x => x.Email == user.Email || x.Phone == user.Phone || (x.Name == user.Name && x.Address == user.Address));

                if (isDuplicated)
                {
                    Debug.WriteLine("The user is duplicated");

                    return Result.Failure("The user is duplicated");
                }

                var giftService = _factory.Create(user.UserType);

                user.Money = giftService.GetDiscount(user.Money);
                user.Email = user.Email.NormalizeEmail();

                await _userRepository.CreateAsync(user);

                Debug.WriteLine("User Created");
                return Result.Success("User Created");

            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return Result.Failure("The user is duplicated");
            }
        }
    }
}