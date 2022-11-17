using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Entitys;
using Sat.Recruitment.Api.Integration;
using Sat.Recruitment.Api.Services;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        readonly IUserBussiness userBussiness;

        public UsersController(UserBusiness userBussiness)
        {
            this.userBussiness = userBussiness;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(User user)
        {
            return await userBussiness.AddUser(user);
        }


    }
    
}
