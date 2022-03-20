using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Controllers.Interface;
using Sat.Recruitment.Api.Logic;
using Sat.Recruitment.Api.Controllers.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {        
        [HttpPost]
        [Route("/create-user")]
        public Result CreateUser(RequestUser usuario)
        {
            List<ValidationResult> validationResultList = new List<ValidationResult>();
            bool validModel = Validator.TryValidateObject(usuario, new ValidationContext(usuario), validationResultList, true);
            if (validModel)
            {
                Ilogic i = new Users();
                return i.CreateUser(usuario);
            }
            else
            {
                return new Result(false, validationResultList.Select(e => e.ErrorMessage).FirstOrDefault());
            }
        }
       
    }
}
