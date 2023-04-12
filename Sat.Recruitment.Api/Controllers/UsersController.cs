using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Business;
using Sat.Recruitment.Api.Models.Dtos;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UsersController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<ResultResponse> CreateUserAsync(UserDto user)
        {
            return await _userBusiness.ProcessCreateUser(user);
        }

    }

}
