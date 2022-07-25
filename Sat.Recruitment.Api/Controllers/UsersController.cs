using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.BusinessLogic.ExternalServices;
using Sat.Recruitment.Models;
using Sat.Recruitment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : Controller
    {
        private readonly IUserSvc _userSvc;

        public UsersController(IUserSvc userSvc)
            => _userSvc = userSvc;

        [HttpPost]
        public async Task<ActionResult> Post(UserModel user)
        {
            var response = await _userSvc.Save(user);
            if (response.IsSuccess)
                return Ok(response.Message);
            else
                return BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        { 
            var response = await _userSvc.GetAll();
            if (response.Count > 0)
                return Ok(response);
            else
                return NotFound();
        }

        [HttpGet("Id")]
        public async Task<ActionResult> Get(Guid Id)
        { 
            var response = await _userSvc.Get(Id);
            if (response != null)
                return Ok(response);
            else
                return NotFound();
        }
    }
}
