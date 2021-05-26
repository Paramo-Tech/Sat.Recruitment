using Business.Strategies;
using Business.Users.Commands;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        public Result CreateUser(User user)
        {
            var validationResultList = new List<ValidationResult>();
            var validModel = Validator.TryValidateObject(user, new ValidationContext(user), validationResultList, true);
            if (!validModel)
            {
                return new Result() { IsSuccess = false, Errors = validationResultList.Select(e => e.ErrorMessage) };
            }

            FactoryMoneyUser factory = new FactoryMoneyUser();
            user.Money = factory.GetMoneyCalculatedByUser(user);
            user.Email = EmailHelper.Normalize(user.Email);
            var readUsers = new ReadUsers();
            var _users = readUsers.GetUsers();
            try
            {
                var isDuplicated = false;
                foreach (var userRead in _users)
                {
                    if (userRead.Email == user.Email
                        ||
                        userRead.Phone == user.Phone)
                    {
                        isDuplicated = true;
                    }
                    else if (userRead.Name == user.Name)
                    {
                        if (userRead.Address == user.Address)
                        {
                            isDuplicated = true;
                            throw new Exception("User is duplicated");
                        }

                    }
                }

                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");

                    return new Result()
                    {
                        IsSuccess = true
                    };
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = new List<string>() { "The user is duplicated" }
                    };
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return new Result()
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "The user is duplicated" }
                };
            }
        }
    }
}
