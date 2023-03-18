using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Business;
using Sat.Recruitment.Api.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserBusiness userBusiness, IMapper mapper, ILogger<UsersController> logger)
        {
            _userBusiness = userBusiness;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<UserRequest>> CreateUser([FromBody] UserRequest request)
        {
            try
            {
                var user = _mapper.Map<User>(request);
                User newUser = await _userBusiness.CreateUser(user);
                UserRequest response = _mapper.Map<UserRequest>(newUser);
                return response;
            }
            catch (DuplicateNameException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An internal error ocurred while processing the request");
                return StatusCode(500, $"Internal error:{ex}");
            }
        }
    }
}
