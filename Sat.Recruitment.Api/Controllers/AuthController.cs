using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(string userName, string password)
        {
            if (!Autenticate(userName, password))
            {
                return Unauthorized();
            }
            var token = _tokenService.TokenGen(userName);
            return Ok(new { token });
        }

        private bool Autenticate(string userName, string password)
        {
            return true;
        }
    }
}
