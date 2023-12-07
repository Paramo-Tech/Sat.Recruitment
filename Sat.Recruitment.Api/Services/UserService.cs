using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services
{
    public class UserService
    {
        private readonly FileService _fileService;

        public UserService()
        {
            _fileService = new FileService();
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

        public string ValidateErrors(string name, string email, string address, string phone)
        {
            string errors = string.Empty;

            if (name == null)
                errors = "The name is required";
            if (email == null)
                errors += " The email is required.";
            if (address == null)
                errors += " The address is required.";
            if (phone == null)
                errors += " The phone is required.";

            return errors;
        }
    }
}
