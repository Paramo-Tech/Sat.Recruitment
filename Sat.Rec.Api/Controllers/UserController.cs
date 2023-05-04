using System.Net;
using Microsoft.AspNetCore.Mvc;
using Sat.Rec.Api.DTO;
using Sat.Rec.Api.Extensions.DTO;
using Sat.Rec.Core.Services.Interfaces;
using Sat.Rec.Models;

namespace Sat.Rec.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public readonly IUserService _userService;
        public Guid transactionId { get; set; }

        /// <summary>
        /// UserController Constructor
        /// </summary>
        /// <param name="userService"></param>
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            transactionId = Guid.NewGuid();
        }

        /// <summary>
        /// Get the list of Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersList()
        {
            _logger.LogInformation($"GetUsersList:Enter {transactionId}");

            var getUsersResult = await _userService.GetAll();
            if (!getUsersResult.Errors.Any())
            {
                _logger.LogInformation($"GetUsersList:Returning OK {transactionId}");

                return Ok(getUsersResult.ResultList);
            }
            else
            {
                var errors = Newtonsoft.Json.JsonConvert.SerializeObject(getUsersResult.Errors);
                _logger.LogInformation($"GetUsersList:Returning InternalServerError and errors({errors}) {transactionId} ");

                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
        }

        /// <summary>
        /// Get User by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(int userId)
        {
            _logger.LogInformation($"GetUserById:Enter {transactionId}");

            var getUserResult = await _userService.GetById(userId);
            if (!getUserResult.Errors.Any())
            {
                if (getUserResult.CustomResultCode == StatusCodes.Status200OK)
                {
                    _logger.LogInformation($"GetUserById:Returning OK {transactionId}");

                    return Ok(getUserResult.SingleResult);
                }
                else
                {
                    _logger.LogInformation($"GetUserById:Returning NotFound {transactionId}");

                    return NotFound();
                }
            }
            else
            {
                var errors = Newtonsoft.Json.JsonConvert.SerializeObject(getUserResult.Errors);
                switch (getUserResult.CustomResultCode)
                {
                    case StatusCodes.Status400BadRequest:
                        _logger.LogInformation($"GetUserById:Returning BadRequest and errors({errors}) {transactionId} ");

                        return BadRequest(string.Join(";", getUserResult.Errors.Select(txt => txt)));
                    default:
                        _logger.LogInformation($"GetUserById:Returning InternalServerError and errors({errors}) {transactionId} ");

                        return StatusCode(StatusCodes.Status500InternalServerError, errors);
                }
            }
        }

        /// <summary>
        /// Add a new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser(CreateUserDTO user)
        {
            _logger.LogInformation($"CreateUser:Enter {transactionId}");

            var createUserResult = await _userService.Create(user.ToModel());

            if (!createUserResult.Errors.Any())
            {
                _logger.LogInformation($"CreateUser:Returning CreatedAtAction UserId:({createUserResult.SingleResult?.UserId}) {transactionId}");

                return CreatedAtAction("GetUserById", new { userId = createUserResult.SingleResult?.UserId }, createUserResult.SingleResult);
            }
            else
            {
                var errors = Newtonsoft.Json.JsonConvert.SerializeObject(createUserResult.Errors);

                if (createUserResult.CustomResultCode == (int)HttpStatusCode.BadRequest)
                {
                    _logger.LogInformation($"CreateUser:Returning BadRequest and errors({errors}) {transactionId}");

                    return BadRequest(string.Join(";", errors));
                }
                else
                {
                    _logger.LogInformation($"CreateUser:Returning InternalServerError and errors({errors}) {transactionId}");

                    return StatusCode(StatusCodes.Status500InternalServerError, errors);
                }
            }
        }

        /// <summary>
        /// Update the User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromQuery] int userId, UpdateUserDTO user)
        {
            _logger.LogInformation($"UpdateUser:Enter {transactionId}");

            var updateUserResult = await _userService.Update(user.ToModel(userId));
            if (!updateUserResult.Errors.Any())
            {
                _logger.LogInformation($"UpdateUser:Returning NoContent {transactionId}");

                return NoContent();
            }
            else
            {
                var errors = Newtonsoft.Json.JsonConvert.SerializeObject(updateUserResult.Errors);

                if (updateUserResult.CustomResultCode == (int)HttpStatusCode.BadRequest)
                {
                    _logger.LogInformation($"UpdateUser:Returning BadRequest and errors({errors}) {transactionId}");

                    return BadRequest(string.Join(";", errors));
                }
                else
                {
                    _logger.LogInformation($"UpdateUser:Returning InternalServerError and errors({errors}) {transactionId}");

                    return StatusCode(StatusCodes.Status500InternalServerError, errors);
                }
            }
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="userId">Id of the user to delete</param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            _logger.LogInformation($"UpdateUser:Enter userId:({userId}){transactionId}");

            var deleteUserResult = await _userService.Delete(userId);
            if (!deleteUserResult.Errors.Any())
            {
                _logger.LogInformation($"UpdateUser:Returning NoContent {transactionId}");

                return NoContent();
            }

            else
            {
                var errors = Newtonsoft.Json.JsonConvert.SerializeObject(deleteUserResult.Errors);

                if (deleteUserResult.CustomResultCode == (int)HttpStatusCode.BadRequest)
                {
                    _logger.LogInformation($"UpdateUser:Returning BadRequest and errors({errors})  {transactionId}");

                    return BadRequest(string.Join(";", errors));
                }
                else
                {
                    _logger.LogInformation($"UpdateUser:Returning InternalServerError and errors({errors})  {transactionId}");

                    return StatusCode(StatusCodes.Status500InternalServerError, errors);
                }
            }
        }
    }
}
