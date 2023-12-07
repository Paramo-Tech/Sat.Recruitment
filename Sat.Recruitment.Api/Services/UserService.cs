using Microsoft.AspNetCore.Hosting;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace Sat.Recruitment.Api.Services
{
    public class UserService
    {
        private readonly FileService _fileService;

        public UserService()
        {
            _fileService = new FileService();
        }

        public void CreateUser(User newUser)
        {         
            if (newUser.UserType == "Normal")
            {
                if (newUser.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    var gif = newUser.Money * percentage;
                    newUser.Money += gif;
                }
                if (newUser.Money < 100 && newUser.Money > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = newUser.Money * percentage;
                    newUser.Money += gif;
                }
            }
            else if (newUser.UserType == "SuperUser")
            {
                if (newUser.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = newUser.Money * percentage;
                    newUser.Money += gif;
                }
            }
            else if (newUser.UserType == "Premium")
            {
                if (newUser.Money > 100)
                {
                    var gif = newUser.Money * 2;
                    newUser.Money += gif;
                }
            }

            newUser.Email = NormalizeEmail(newUser.Email);

            string stringifiedUser = $"{newUser.Name},{newUser.Email},{newUser.Phone},{newUser.Address},{newUser.UserType},{newUser.Money}";
            _fileService.SaveUserIntoFile(stringifiedUser);
        }

        public List<User> GetUserListFromFile()
        {
            try
            {
                List<User> users = new List<User>();
                var reader = _fileService.ReadUsersFromFile();

                while (reader.Peek() >= 0)
                {                    
                    var line = reader.ReadLineAsync().Result.Split(',');

                    if (line.Length > 1 && !line[0].Equals(""))
                    {
                        var user = new User
                        {
                            Name = line[0].ToString(),
                            Email = line[1].ToString(),
                            Phone = line[2].ToString(),
                            Address = line[3].ToString(),
                            UserType = line[4].ToString(),
                            Money = decimal.Parse(line[5].ToString()),
                        };
                        users.Add(user);
                    }
                }
                    

                reader.Close();

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            email = string.Join("@", new string[] { aux[0], aux[1] });

            return email;
        }
    }
}
