using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Features.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Features.Users
{
    public partial class UsersController : ApiControllerBase
    {
        private readonly IUserDataService _userDataService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserDataService userDataService, ILogger<UsersController> logger)
        {
            _userDataService = userDataService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Users
        ///     {
        ///         "name": "Homer Simpson",
        ///         "email": "homer@compuglobalhypermeganet.com",
        ///         "address": "742 Evergreen Terrace",
        ///         "phone": "+1 5555555555",
        ///         "userType": "normal",
        ///         "money": 100
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Nothing</returns>
        /// <response code="201">Returns "User Created" message</response>
        /// <response code="400">
        /// Returns validation errors if data is missing or "The user is duplicated" if user is duplicated
        /// </response>
        /// <response code="500">
        /// Returns exception details for debugging mode, otherwise "Unhandled error" message
        /// </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var (isSuccess, error, newUser) = UserFactory.Create(
                request.UserType,
                request.Name,
                request.Email,
                request.Address,
                request.Phone,
                request.Money);
            if (!isSuccess) return BadRequest(error);

            var users = await _userDataService.GetAll();

            var isDuplicated = users.Any(user =>
                (user.Email == newUser.Email || user.Phone == newUser.Phone) ||
                (user.Name == newUser.Name && user.Address == newUser.Address)
            );

            if (isDuplicated)
            {
                _logger.LogInformation("The user is duplicated. E-mail: {email}", request.Email);
                return BadRequest("The user is duplicated");
            }
            else
            {
                _logger.LogInformation("User Created. E-mail: {email}", request.Email);
                return Created("User Created");
            }
        }
    }
}
