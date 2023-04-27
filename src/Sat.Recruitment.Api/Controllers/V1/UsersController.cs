using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Common.Extensions;
using Sat.Recruitment.Application.Users.Commnads;
using Sat.Recruitment.Application.Users.Models;
using Sat.Recruitment.Application.Users.Queries;

namespace Sat.Recruitment.Api.Controllers.V1
{
    /// <summary>
    /// This API handles all logic related to the Users
    /// </summary>
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        private const string UsersCache = "UsersCache";

        private readonly IMemoryCache _memoryCache;
        private ISender _mediator = null!;

        private ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        public UsersController(ILogger<UsersController> logger, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Creates a new User based on the query input
        /// </summary>
        /// <param name="name">User's full name</param>
        /// <param name="email">Valid email address</param>
        /// <param name="address">User's address lines</param>
        /// <param name="phone">Valid phone number, including country code</param>
        /// <param name="userType">UserTypes: Normal, SuperUser or Premium</param>
        /// <param name="money">Numeric value that represents the User's money</param>
        /// <returns>Results object</returns>
        [Obsolete]
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("create-user")]
        public async Task<Models.Result> CreateUser([FromQuery] string name, string email, string address, string phone, string userType, string money)
        {
            var result = new Models.Result();

            if (!decimal.TryParse(money, out decimal parsedMoney))
            {
                ModelState.AddModelError(string.Empty, "The money parameter is invalid.");
            }

            var command = new CreateUserCommand
            {
                Address = address,
                Email = email,
                Phone = phone,
                Money = parsedMoney,
                Name = name,
                UserType = userType.ToUserTypes()
            };

            if (!TryValidateModel(command))
            {
                result.Errors = string.Join('|', ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

                return result;
            }

            (var response, _) = await Mediator.Send(command);

            result.IsSuccess = response.Succeeded;
            result.Errors = string.Join('|', response.Errors);

            if (response.Succeeded)
                _memoryCache.Remove(UsersCache);

            return result;
        }

        /// <summary>
        /// Creates a new User based on the command input
        /// </summary>
        /// <param name="command">The CreateUserCommand object has the information required to create a new user</param>
        /// <returns>Result object</returns>
        [HttpPost]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("create-user")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            (var response, _) = await Mediator.Send(command);

            if (response.Succeeded)
            {
                _memoryCache.Remove(UsersCache);
                return Created(string.Empty, response);
            }

            return Conflict(response);
        }

        /// <summary>
        /// Returns all the Users
        /// </summary>
        /// <param name="query"></param>
        /// <returns>An array of Users</returns>
        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("get-users")]
        public async Task<ActionResult<IList<UserViewModel>>> GetUsers([FromQuery] GetUsersQuery query)
        {
            var response = await _memoryCache.GetOrCreateAsync(UsersCache, async x => await Mediator.Send(query));

            return Ok(response);
        }
    }
}
