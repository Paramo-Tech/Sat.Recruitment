using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Helpers;
using Sat.Recruitment.Api.Services.Interfaces;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUser _userService;
        public UsersController( IUser userService )
        {
            _userService = userService;
        }

        [HttpPost]
        //[Route("/create-user")]
        public async Task<IActionResult> CreateUser( userCreateDTO dto)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var response = await _userService.InsertAndSaveAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


    

    }

}
