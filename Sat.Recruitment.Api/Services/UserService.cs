using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Xml.Linq;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Entities.Dto;
using Sat.Recruitment.Api.Services.Helpers;

namespace Sat.Recruitment.Api.Services
{
	public class UserService
	{
        private readonly List<User> _users = new List<User>();
        public UserService()
		{
		}

        private void calculateUserMoney(ref UserDTO newUser)
        {
            decimal percentage = 0;
            decimal gif = 0;

            switch (newUser.userType)
            {
                case "Normal":
                    if (newUser.money > 100)
                    {
                        percentage = Convert.ToDecimal(0.12);
                    } else 
                    if (newUser.money > 10 && newUser.money < 100)
                    {
                        percentage = Convert.ToDecimal(0.8);
                    }
                    break;

                case "SuperUser":
                    if (newUser.money > 100)
                    {
                        percentage = Convert.ToDecimal(0.20);
                    }
                    break;
            }

            if(newUser.userType == "Normal" || newUser.userType == "Superuser")
            {
                gif = newUser.money * percentage;
            } else if(newUser.userType=="Premium")
            {
                gif = newUser.money * 2;
            }

            newUser.money += gif;
        }

        private string normalizeEmail(string email)
        {
            var mailAddress = new MailAddress(email);
            return mailAddress.Address.ToLower();
        }

        private Boolean isUserDuplicate(UserDTO newUser)
        {
            var foundUser = _users.Find(u => u.Email == newUser.email || u.Phone == newUser.phone || (u.Name == newUser.name && u.Address == newUser.address));
            return foundUser != null;
        }

        public Result CreateUser(UserDTO userDto) {
            Result result = new Result();

            calculateUserMoney(ref userDto);

            var newUser = new User
            {
                Name = userDto.name,
                Email = normalizeEmail(userDto.email),
                Address = userDto.address,
                Phone = userDto.phone,
                UserType = userDto.userType,
                Money = userDto.money
            };


            var reader = new UsersReader().ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();

            if (!isUserDuplicate(userDto))
            {
                // User does not exist
                Debug.WriteLine("User Created");

                result.IsSuccess = true;
                result.Errors = "User Created";
            } else
            {
                Debug.WriteLine("The user is duplicated");

                result.IsSuccess = false;
                result.Errors = "The user is duplicated";
            }
            return result;
        }
    }
}

