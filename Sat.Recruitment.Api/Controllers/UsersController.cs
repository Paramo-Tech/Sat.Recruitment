using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.BLL;
using Sat.Recruitment.BLL.Dto;
using Sat.Recruitment.BLL.interfaces;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService service, ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var newUser = new CreateUserDTO
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };

            try
            {
                var result = await _service.CreateUser(newUser);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred in user controller");
                return new Result()
                {
                    IsSuccess = false,
                    Errors = ex.Message
                };
            }
        }
    }
}
