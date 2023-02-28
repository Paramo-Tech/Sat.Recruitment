using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Policy;
using System;
using System.IO.Pipes;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        readonly string path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

        public Result createUser(User user)
        {
            Result ret = new Result();
            var errors = "";
            if (ValidateErrors(user, ref errors))
            {
                ret.IsSuccess = false;
                ret.Errors = errors;
                return ret;
            }

            user.money = calculateMoney(user);
            user.email = normalizeEmail(user.email);

            if (isUserDuplicated(user))
            {
                ret.IsSuccess = false;
                ret.Errors = "The user is duplicated";
            }
            else
            {
                if (InsertUser(user))
                {
                    ret.IsSuccess = true;
                    ret.Errors = "User Created";
                }
                else
                {
                    ret.IsSuccess = false;
                    ret.Errors = "User not Created";
                }
            }
            return ret;
        }
                       
        public Task<Result> CreateUserAsync(User user)
        {
            Result ret = new Result();
            var errors = "";
            if (ValidateErrors(user, ref errors))
            {
                ret.IsSuccess = false;
                ret.Errors = errors;
                return Task.FromResult(ret);
            }

            user.money = calculateMoney(user);
            user.email = normalizeEmail(user.email);

            if (isUserDuplicated(user))
            {
                ret.IsSuccess = false;
                ret.Errors = "The user is duplicated";
            }
            else
            {
                if (InsertUser(user))
                {
                    ret.IsSuccess = true;
                    ret.Errors = "User Created";
                }
                else
                {
                    ret.IsSuccess = false;
                    ret.Errors = "User not Created";
                }
            }
            return Task.FromResult(ret);
        }

        public List<User> GetAllUsers()
        {
            return getAllUsers();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return Task.FromResult(getAllUsers());
        }

        private bool ValidateErrors(User user, ref string errors)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            List<string> errmsg = new List<string>();

            if (string.IsNullOrEmpty(user.name))
                errmsg.Add("The name is required");

            if (string.IsNullOrEmpty(user.email))
                errmsg.Add("The email is required");

            if (string.IsNullOrEmpty(user.address))
                errmsg.Add("The address is required");

            if (string.IsNullOrEmpty(user.phone))
                errmsg.Add("The phone is required");

            if (!regex.Match(user.email).Success)
                errmsg.Add("The email is in wrong format");

            errors = string.Join(",", errmsg);
            return errmsg.Count > 0;
        }

        private decimal calculateMoney(User user)
        {

            switch (user.userType)
            {
                case "Normal":
                    if (user.money > 100)
                        user.money = user.money + (user.money * 0.12M);
                    else
                        if (user.money > 10)
                        user.money = user.money + (user.money * 0.8M);
                    break;

                case "SuperUser":
                    if (user.money > 100)
                        user.money = user.money + (user.money * 0.20M);
                    break;

                case "Premium":
                    if (user.money > 100)
                        user.money = user.money + (user.money * 2M);
                    break;
            }
            return user.money;
        }

        private string normalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            email = string.Join("@", new string[] { aux[0], aux[1] });
            return email;
        }

        private bool isUserDuplicated(User user)
        {
            List<User> _users = new List<User>();
            _users = GetAllUsers();
            return _users.Exists(x => x.email == user.email || x.phone == user.phone || x.name == user.name || x.address == user.address);
        }

        private List<User> getAllUsers()
        {

            List<User> _users = new List<User>();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            try
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    if (line.Trim().Length > 0)
                    {
                        _users.Add(new User
                        {
                            name = line.Split(',')[0].ToString(),
                            email = line.Split(',')[1].ToString(),
                            phone = line.Split(',')[2].ToString(),
                            address = line.Split(',')[3].ToString(),
                            userType = line.Split(',')[4].ToString(),
                            money = decimal.Parse(line.Split(',')[5].ToString()),
                        });
                    }
                }
            }
            finally
            {
                reader.Close();
                fileStream.Close();
            }

            return _users;
        }
        private bool InsertUser(User user)
        {
            bool ret = false;
            FileStream fileStream = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fileStream);
            try
            {
                sw.WriteLine(user.ToString());
            }
            finally
            {
                sw.Close();
                fileStream.Close();
                ret = true;
            }
            return ret;
        }
    }
}
