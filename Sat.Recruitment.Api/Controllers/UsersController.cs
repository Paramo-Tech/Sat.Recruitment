using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sat.Recruitment.Api.ViewModel;
using Sat.Recruitment.Domain.Contracts.Services;
using Sat.Recruitment.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UsersController(IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        
        public async Task<ActionResult> CreateUser([FromBody] UserViewModel userViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = _mapper.Map<User>(userViewModel);

                if (await _userService.ExistsAsync(user))
                {
                    log.Error("User Duplicated" + JsonConvert.SerializeObject(user));
                    return StatusCode(StatusCodes.Status409Conflict, "The user is already in the list");
                }
                else
                {
                    await _userService.AddAsync(user);
                    return Ok("User Created");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error, please contact IT Team");
            }

        }
    }

}
