using Business.Strategies;
using Business.Users.Commands;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUserAsync(User user)
        {
            var readUsers = new ReadUsers();
            var userTask = readUsers.GetUsersAsync();

            var validationResultList = new List<ValidationResult>();
            var validModel = Validator.TryValidateObject(user, new ValidationContext(user), validationResultList, true);
            if (!validModel)
            {
                return new Result() { IsSuccess = false, Errors = validationResultList.Select(e => e.ErrorMessage) };
            }

            var factory = new FactoryMoneyUser();
            user.Money = factory.GetMoneyCalculatedByUser(user);
            user.Email = EmailHelper.Normalize(user.Email);

            var users = await userTask;
            var result = new Result() { IsSuccess = true };
            if (users.Any(u => u.Email == user.Email && u.Name == u.Name))
            {
                result = new Result() { IsSuccess = false, Errors = new List<string>() { "The user is duplicated" } };
            }
            return result;
        }
    }
}
