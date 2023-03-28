using MediatR;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Exceptions;
using Sat.Recruitment.Core.Models.User;

namespace Sat.Recruitment.Kernel.Features.Users.CreateUserCommand
{
    /// <summary>
    /// Handler to create new user
    /// </summary>
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, object>
    {
        private readonly IUserService _userService;
        private readonly IUserCalculateGiftValue _userCalculateGiftValue;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService">User service to save data</param>
        /// <param name="userCalculateGiftValue">Object to calculate gift value</param>
        public CreateUserHandler(IUserService userService, IUserCalculateGiftValue userCalculateGiftValue)
        {
            _userService = userService;
            _userCalculateGiftValue = userCalculateGiftValue;
        }

        /// <summary>
        /// Handler method
        /// </summary>
        /// <param name="request">Request parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public async Task<object> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                UserId = Guid.NewGuid().ToString(),
                Address = request.Address,
                Email = request.Email,
                Money = request.Money,
                Name = request.Name,
                Phone = request.Phone,
                UserType = request.UserType
            };
            var users = _userService.GetAsync().Result;
            //Validate duplicated user
            ValidateDuplicatedUser(request, users);

            //Calculate Gift Value
            var gift = _userCalculateGiftValue.GetGiftValue(request.UserType?? "", request.Money);
            user.Money += gift;

            //Add user
            await _userService.AddAsync(user);
            return user.Id?? "0";
        }

        /// <summary>
        /// Validate if user is duplicated
        /// </summary>
        /// <param name="request">Request command</param>
        /// <param name="users">List of users</param>
        /// <exception cref="UserDuplicatedException"></exception>
        private void ValidateDuplicatedUser(CreateUserRequest request, IEnumerable<IUser>? users)
        {
            if (users is not null)
            {
                if (users.Any(u => (u.Email is not null && u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)) ||
                                   (u.Phone is not null && u.Phone.Equals(request.Phone, StringComparison.OrdinalIgnoreCase))))
                    throw new UserDuplicatedException("Email or Phone already exist in the database.");

                if (users.Any(u => (u.Name is not null && u.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)) &&
                                   (u.Address is not null && u.Address.Equals(request.Address, StringComparison.OrdinalIgnoreCase))))
                    throw new UserDuplicatedException("Name and Addres already exist in database.");
            }
        }
    }
}
