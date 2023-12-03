using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Exceptions;
using System.Web;


namespace Sat.Recruitment.Api.Controllers
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> getUsers(string name, string email, string address, string phone, string userType, string money)
        {
            name = HttpUtility.UrlDecode(name, System.Text.Encoding.UTF8);
            email = HttpUtility.UrlDecode(email, System.Text.Encoding.UTF8);
            address = HttpUtility.UrlDecode(address, System.Text.Encoding.UTF8);
            phone = HttpUtility.UrlDecode(phone, System.Text.Encoding.UTF8);
            userType = HttpUtility.UrlDecode(userType, System.Text.Encoding.UTF8);
            return _userService.getUsers(name, email, address, phone, userType, money);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateUser(User newUser)
        {

            try
            {
                _userService.createUser(newUser);
                return Created("", new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                });
            }
            catch (DuplicatedUserException)
            {
                return Conflict(new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                });
            }


        }

    }

}
