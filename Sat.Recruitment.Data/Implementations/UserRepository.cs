using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Sat.Recruitment.Data.DB;
using Sat.Recruitment.Data.Definitions;
using Sat.Recruitment.Domain.Domains;
using Sat.Recruitment.Domain.ExtensionMethods;

namespace Sat.Recruitment.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private DataBase _database;
        public UserRepository()
        {
            _database = DataBase.GetInstance;
        }
        public List<User> GetAll()
        {
            List<User> response = new List<User>();
            var reader = _database.ReadUsersFromFile();

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
                response.Add(user);
            }
            reader.Close();

            return response; 
        }

        public async Task<Result> ProcessingNewUserAsync(User newUser)
        {
            var errors = "";
            ValidateErrors(newUser, ref errors);

            if (!string.IsNullOrEmpty(errors))
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };

            newUser.Email = newUser.Email.NormalizeEmail();

            List<User> _users =  GetAll();


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

                    return new Result()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }
        }

        public void ValidateErrors(User user, ref string errors)
        {
            if (user.Name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (user.Email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (user.Address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (user.Phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";
        }
    }
}
