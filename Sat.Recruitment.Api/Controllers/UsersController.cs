using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using Sat.Recruitment.Api.ApiModels;
using Sat.Recruitment.Api.Services.Contracts;
using Sat.Recruitment.Api.Services.DataObjects;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/ping")]
        public async Task<string> Test()
        {
            return await Task.Run(() => "pong");
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(CreateUserRequest model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(r => r.Errors)
                    .Select(r => r.ErrorMessage)
                    .Aggregate(string.Empty, (p, s) => $"{p} {s}");

                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };
            }

            //  var errors = "";

            // ValidateErrors(model.Name, model.Email, model.Address, model.Phone, ref errors);
            //
            // if (errors != null && errors != "")
            //     return new Result()
            //     {
            //         IsSuccess = false,
            //         Errors = errors
            //     };


            var dto = new CreateUserDto();
            dto.InjectFrom(model);

            try
            {
                var (isDuplicated, errorMessage) = _userService.CreateUser(dto);
                Debug.WriteLine(errorMessage);

                return new Result()
                {
                    IsSuccess = !isDuplicated,
                    Errors = errorMessage
                };
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
                return new Result()
                {
                    IsSuccess = false,
                    Errors = err.Message
                };
            }
        }

        //Validate errors
        private void ValidateErrors(string name, string email, string address, string phone, ref string errors)
        {
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }
    }
}