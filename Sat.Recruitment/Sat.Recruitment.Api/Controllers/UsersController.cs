using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Sat.Recruitment.Api.Application.Interfaces;
using Sat.Recruitment.Api.Application.Request;
using Sat.Recruitment.Api.Middleware.Logs;

namespace Sat.Recruitment.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserApplication _application;

        private Logs _logs;
        public UsersController(IUserApplication application,IWebHostEnvironment environment)
        {
            _application=application;
            _logs = new Logs(environment);
            _logs.Lineas = new List<string>();
            _logs.Proceso = "USERS";
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<IActionResult> CreateUser(string name,string email , string address, string phone ,string UserType, string money)
        {
            Services.Responses.Result result = new Services.Responses.Result();
            try
            {
                _logs.Lineas.Add($"Request :  name {name},email {email},address {address} ,phone {phone}, UserType {UserType} ,money {money} ");
                _logs.GrabarLogs();
                result = await _application.AddUser(new UserDTO(name,email,address,phone, UserType,money));
            }
            catch (Exception ex)
            {
                _logs.Lineas.Add($"catch : {ex.Message} ");
                _logs.GrabarLogs();
                result.Errors = ex.Message;
                result.IsSuccess = false;
            }

            return Ok(result);
        }
    }
   
}
