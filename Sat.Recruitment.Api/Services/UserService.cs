using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                    //If new user is normal and has more than USD100
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
            if (newUser.UserType == "SuperUser")
            {
                if (newUser.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = newUser.Money * percentage;
                    newUser.Money += gif;
                }
            }
            if (newUser.UserType == "Premium")
            {
                if (newUser.Money > 100)
                {
                    var gif = newUser.Money * 2;
                    newUser.Money += gif;
                }
            }

            newUser.Email = NormalizeEmail(newUser.Email);
        }

        public List<User> GetUserListFromFile()
        {
            List<User> users = new List<User>();

            var reader = _fileService.ReadUsersFromFile();

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
                users.Add(user);
            }

            reader.Close();

            return users;
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
