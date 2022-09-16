using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.BusinessLogic.Model;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.BusinessLogic;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IApplicationBL _aplicationBL;

        public UsersController(IApplicationBL aplicationBL)
        {
            this._aplicationBL = aplicationBL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(this._aplicationBL.GetUsers());
        }

        [HttpGet]
        [Route("{email}")]
        public ActionResult<IEnumerable<User>> Get(string email)
        {
            var user = this._aplicationBL.GetUser(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            if (user == null)
                return NotFound();

            return Ok(user);

        }

        [HttpPost]
        public ActionResult Create([FromBody] User user)
        {
            try
            {
                this._aplicationBL.SaveUser(user);
                return StatusCode(201);
            }
            catch(EVisibleException e)
            {
                return StatusCode(500, e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        
    }
}
