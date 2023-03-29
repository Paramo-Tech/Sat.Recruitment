using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Sat.Recruitment.Api.Logic.Interfaces;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sat.Recruitment.Api.Logic.Business
{
    public class Users : IUsers
    {
        private delegate decimal calculation(decimal money);
        private Dictionary<string, calculation> _calculations;

        public Users() 
        {
            _calculations = new Dictionary<string, calculation>();
            _calculations.Add("normal", CalculateByNormalUser);
            _calculations.Add("superuser", CalculateBySuperUserUser);
            _calculations.Add("premium", CalculateByPremiumUser);
        }

        public decimal CalculateMoneyByUserType(string userType, decimal currentMoney)
        {
            if(!_calculations.ContainsKey(userType.ToLower()))
            {
                throw new Exception("UserType not exists.");
            }

            return _calculations[userType.ToLower()](currentMoney);
        }

        public string NormalizeEmail(string email)
        {
            string result = string.Empty;

            try
            {
                //Normalize email
                var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                result = string.Join("@", new string[] { aux[0], aux[1] });
            }
            catch 
            {
                throw new Exception("Email has no the correct format");
            }

            return result;
        }

        public async Task<List<UserRequest>> ReadUsersFromFile()
        {
            var result = new List<UserRequest>();

            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (reader.Peek() >= 0)
                {
                    var line = (await reader.ReadLineAsync()).Split(',', StringSplitOptions.RemoveEmptyEntries);

                    var user = new UserRequest
                    {
                        Name = line[0].ToString(),
                        Email = line[1].ToString(),
                        Phone = line[2].ToString(),
                        Address = line[3].ToString(),
                        UserType = line[4].ToString(),
                        Money = decimal.Parse(line[5].ToString()),
                    };
                    result.Add(user);
                }
            }

            return result;
        }

        private decimal CalculateByNormalUser(decimal money)
        {
            decimal gif = 0;

            if (money > 100)
            {   
                gif = money * 0.12M;
            }
            else if (money > 10 && money < 100)
            {
                gif = money * 0.8M;
            }

            return money + gif;
        }

        private decimal CalculateBySuperUserUser(decimal money)
        {
            decimal gif = 0;

            if (money > 100)
            {
                gif = money * 0.20M;
            }

            return money + gif;
        }

        private decimal CalculateByPremiumUser(decimal money)
        {
            decimal gif = 0;

            if (money > 100)
            {
                gif = money * 2;
            }

            return money + gif;
        }

        public List<string> RequiredFieldsValidation(UserRequest user)
        {
            var result = new List<string>();

            if (user.Name.IsNullOrEmptyOrWhiteSpaces())
                //Validate if Name is null
                result.Add("The name is required");
            if (user.Email.IsNullOrEmptyOrWhiteSpaces())
                //Validate if Email is null
                result.Add("The email is required");
            if (user.Address.IsNullOrEmptyOrWhiteSpaces())
                //Validate if Address is null
                result.Add("The address is required");
            if (user.Phone.IsNullOrEmptyOrWhiteSpaces())
                //Validate if Phone is null
                result.Add("The phone is required");

            return result;
        }
    }
}
