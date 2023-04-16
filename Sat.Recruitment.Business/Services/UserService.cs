using Sat.Recruitment.Business.Contracts;
using Sat.Recruitment.Business.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Sat.Recruitment.Business.Services
{
    public class UserService : IUserService
    {
        private readonly List<UserModel> _users = new List<UserModel>();

        #region Methods

        /// <summary>
        /// Create a new user.
        /// </summary>        
        /// <param name="user">The <see cref="UserModel"/> model.</param>
        /// <returns>The <see cref="ResultModel"/>.</returns>
        public ResultModel CreateUser(UserModel user)
        {
            var Message = "";

            ValidateMessage(user, ref Message);

            if (Message != null && Message != "")
                return new ResultModel()
                {
                    Data = user,
                    IsSuccess = false,
                    Message = Message
                };

            user.Money = AssignMoney(user);
            ReadUsersFromFile();

            user.Email = NormalizeEmail(user.Email);
            

            IsDuplicated(_users, user, ref Message);

            if(Message == "")
            {
                return new ResultModel()
                {
                    Data = user,
                    IsSuccess = true,
                    Message = "User Created"
                };
            }
            else
            {
                return new ResultModel()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = Message
                };
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Validate required properties
        /// </summary>
        /// <param name="user">The <see cref="UserModel"/> model.</param>
        /// <param name="Message">The error variable.</param>        
        private void ValidateMessage(UserModel user, ref string Message)
        {
            if (user.Name == "")
                Message = "The name is required";
            if (user.Email == "")
                Message += " The email is required";
            if (user.Address == "")
                Message += " The address is required";
            if (user.Phone == "")
                Message += " The phone is required";
        }

        /// <summary>
        /// Read users from a file
        /// </summary>        
        /// <returns>The <see cref="StreamReader"/> of users.</returns>
        public virtual void ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            var text = File.ReadAllText(path);
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                var userModel = new UserModel
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(userModel);
            }
        }

        /// <summary>
        /// Validate if a user already exists.
        /// </summary>
        /// <param name="_users">List of users <see cref="UserModel"/>.</param>
        /// <param name="Message">The error variable.</param>        
        private void IsDuplicated(List<UserModel> _users, UserModel userModel ,ref string Message)
        {
            var isDuplicated = false;
            foreach (var user in _users)
            {
                if ((user.Email == userModel.Email || user.Phone == userModel.Phone) || (user.Name == userModel.Name && user.Address == userModel.Address)) isDuplicated = true;

            }
            if (isDuplicated) Message += "The user is duplicated";
        }

        /// <summary>
        /// Assign Money depending on user type
        /// </summary>
        /// <param name="user">The <see cref="UserModel"/> model.</param>
        /// <returns>The assigned money.</returns>
        public decimal AssignMoney(UserModel user)
        {
            switch (user.UserType)
            {
                case "Normal":
                    if (user.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        var gif = user.Money * percentage;
                        user.Money += gif;
                    }
                    if (user.Money < 100 && user.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = user.Money * percentage;
                        user.Money += gif;
                    }                    
                    break;
                case "SuperUser":
                    if (user.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = user.Money * percentage;
                        user.Money += gif;
                    }
                    break;
                case "Premium":
                    if (user.Money > 100)
                    {
                        var gif = user.Money * 2;
                        user.Money += gif;
                    }
                    break;
                default:
                    break;
            }

            return user.Money;
        }

        /// <summary>
        /// Normalize Email.
        /// </summary>
        /// <param name="email">The user email.</param>
        /// <returns>The normalize email.</returns>
        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            email = string.Join("@", new string[] { aux[0], aux[1] });
            return email;
        }
        #endregion
    }
}
