
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Application.Core;
using Sat.Recruitment.Application.Dtos;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Data;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Application
{
    public class UserService : IUserService
    {
        public async Task<Result<object>> CreateUser(CreateUserDto userDto)
        {
            var newUser = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = userDto.UserType,
                Money = decimal.Parse(userDto.Money)
            };
            var userData = new UserData();

            if (newUser.UserType == "Normal")
            {
                if (decimal.Parse(userDto.Money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = decimal.Parse(userDto.Money) * percentage;
                    newUser.Money = newUser.Money + gif;
                }
                if (decimal.Parse(userDto.Money) < 100)
                {
                    if (decimal.Parse(userDto.Money) > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = decimal.Parse(userDto.Money) * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                }
            }
            if (newUser.UserType == "SuperUser")
            {
                if (decimal.Parse(userDto.Money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = decimal.Parse(userDto.Money) * percentage;
                    newUser.Money = newUser.Money + gif;
                }
            }
            if (newUser.UserType == "Premium")
            {
                if (decimal.Parse(userDto.Money) > 100)
                {
                    var gif = decimal.Parse(userDto.Money) * 2;
                    newUser.Money = newUser.Money + gif;
                }
            }

            var users = await userData.GetUsersAsync();

            //Normalize email
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

            try
            {
                var isDuplicated = false;
                foreach (var user in users)
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
                            throw new Exception("User is duplicated");
                        }

                    }
                }

                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");

                    return new Result<object>()
                    {
                        IsSuccess = true,
                        Value = "User Created"
                    };
                }

                Debug.WriteLine("The user is duplicated");

                return new Result<object>()
                {
                    IsSuccess = false,
                    Error = "The user is duplicated"
                };
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return new Result<object>()
                {
                    IsSuccess = false,
                    Error = "The user is duplicated"
                };
            }
        }
    }
}
