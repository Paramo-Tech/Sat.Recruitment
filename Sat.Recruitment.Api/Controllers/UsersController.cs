using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    /**
     * Supuestos:
     * Se asume que la uri del endpoint se puede cambiar asi matchea con las practicas REST
     * Se asume que el UserType debe ser del tipo Normal, SuperUser, o Premium.
     * 
     * 
     * */

    //TODO: Poner el tipo de string aceptado en el request para UserType
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        //addmapper
        public UsersController(IUserService userService, ILogger<UsersController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="requestUser"></param>
        /// <response code="201">Returns success creating new user</response>
        /// <response code="400">An error</response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Produces("application/json")]
        public async Task<ActionResult<Response>> CreateUser([FromBody]UserDTO requestUser)
        {
            var userValidator = new UserValidator();
            var validationResult = userValidator.Validate(requestUser);

            if (!validationResult.IsValid)
                return BadRequest(new Response { IsSuccess = false, Message = validationResult.Errors.FirstOrDefault().ErrorMessage });

            try
            {
                var user = _mapper.Map<User>(requestUser);

                var result = await _userService.Create(user);

                if (!result)
                    return BadRequest(new Response { IsSuccess = false, Message = "The user is duplicated" });

                //return 201 for created resource
                return CreatedAtAction(nameof(CreateUser), new Response { IsSuccess = true, Message = "User Created" });
            }
            catch(Exception ex)
            {
                _logger.LogError("An error ocured", ex);
                return BadRequest(new Response { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
