using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Models.Factory;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        private readonly IUserFactory _userFactory;

        public UsersController(IUserFactory userFactory)
        {
            this._userFactory = userFactory;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<ActionResult> CreateUser(UserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IUser newUser = _userFactory.CreateUser(dto);

            newUser.Money = newUser.Money + newUser.Gift;

            var reader = ReadUsersFromFile();

            //Normalize email
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var datos = line.Split(',');
                var user = new User
                {
                    Name = datos[0].ToString(),
                    Email = datos[1].ToString(),
                    Phone = datos[2].ToString(),
                    Address = datos[3].ToString(),
                    UserType = datos[4].ToString(),
                    Money = decimal.Parse(datos[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();
            try
            {
                var isDuplicated = false;
                foreach (var user in _users)
                {
                    if (user.Email == newUser.Email
                        ||
                        user.Phone == newUser.Phone)
                    {
                        isDuplicated = true;
                    }
                    else if (user.Name == newUser.Name)
                    {
                        if (user.Address == newUser.Address)
                        {
                            isDuplicated = true;
                            throw new Exception("User is duplicated");
                        }

                    }
                }

                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");

                    return Ok("User Created");
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return Conflict("The user is duplicated");
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return Conflict("The user is duplicated");
            }

        }

    }
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}
