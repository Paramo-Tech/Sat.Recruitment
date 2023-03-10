using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Sat.Recruitment.Web.Controllers
{
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userServices)
        {
            _userService = userServices;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto userDto)
        {
            // Validation is call using IValidableDto with ValidationFilter

            var newUser = await _userService.Insert(userDto);
            if (newUser == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return base.Ok(newUser);
        }

        [HttpGet("user-type")]
        [Authorize(Roles = "SuperUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> GetUserTypeByEmail([Required]string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return BadRequest(SatRecruitmentConstants.ErrorMsgEmailRequired);
            }

            var userType = await _userService.GetUserTypeByEmail(userEmail);

            if (string.IsNullOrWhiteSpace(userType))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(userType);
        }
    }
}
