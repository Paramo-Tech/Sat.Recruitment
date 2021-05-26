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

        private readonly List<User> _users = new List<User>();
        public UsersController()
        {
        }

        [HttpPost]
        [Route("/create-user")]
        public Result CreateUser(User user)
        {
            var validationResultList = new List<ValidationResult>();
            bool validModel = Validator.TryValidateObject(user, new ValidationContext(user), validationResultList);
            if (!validModel)
            {
                return new Result() { IsSuccess = false, Errors = validationResultList.Select(e => e.ErrorMessage) };
            }

            Application.FactoryMoneyUser factory = new Application.FactoryMoneyUser();
            user.Money = factory.GetMoneyCalculatedByUser(user);

            var reader = ReadUsersFromFile();

            //Normalize email
            var aux = user.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            user.Email = string.Join("@", new string[] { aux[0], aux[1] });

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var userToAdd = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(userToAdd);
            }
            reader.Close();
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
