using System;
using System.Collections.Generic;
using System.Linq;
using Sat.Recruitment.Api.ApiModels;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Services.Contracts;
using Sat.Recruitment.Api.Services.DataObjects;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IStoreServices _storeServices;

        public UserService(IStoreServices storeServices)
        {
            _storeServices = storeServices;
        }
        
        public (bool isDuplicated, string resultMessage) CreateUser(CreateUserDto dto)
        {
            var newUser = FactoryUser(dto);

            var users= GetUsers();
            
            var dup = HasDuplicatedUser(users, newUser);

            return (dup, dup ? "The user is duplicated" : "User Created");
        }
        

        private  bool HasDuplicatedUser(List<User> users, User newUser)
        {
          //  var isDuplicated = false;

            return users
                .Any(u => (u.Email == newUser.Email || u.Phone == newUser.Phone) ||
                          (u.Name == newUser.Name && u.Address == newUser.Address));
            //
            // foreach (var currentUser in users)
            // {
            //     if (currentUser.Email == newUser.Email
            //         ||
            //         currentUser.Phone == newUser.Phone)
            //     {
            //         isDuplicated = true;
            //     }
            //     else if (currentUser.Name == newUser.Name)
            //     {
            //         if (currentUser.Address == newUser.Address)
            //         {
            //             isDuplicated = true;
            //             throw new Exception("User is duplicated");
            //         }
            //     }
            // }
            //
            // return isDuplicated;
        }

        private User FactoryUser(CreateUserDto model)
        {
            
            //TODO: considerar patron estrageia para calcular el valor de Money | considerar algun servicio normalizador de email.
            var newUser = new User
            {
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                Phone = model.Phone,
                UserType = model.UserType,
                Money = model.Money
            };

            switch (newUser.UserType)
            {
                case UserType.Normal:
                {
                    if (model.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        var gif = model.Money * percentage;
                        newUser.Money += gif;
                    }

                    if (model.Money < 100)
                    {
                        if (model.Money > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = model.Money * percentage;
                            newUser.Money += gif;
                        }
                    }

                    break;
                }
                case UserType.SuperUser:
                {
                    if (model.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = model.Money * percentage;
                        newUser.Money += gif;
                    }

                    break;
                }
                case UserType.Premium:
                {
                    if (model.Money > 100)
                    {
                        var gif = model.Money * 2;
                        newUser.Money += gif;
                    }

                    break;
                }
            }


            //Normalize email
            var aux = newUser.Email.Split(new char[] {'@'}, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] {aux[0], aux[1]});
            return newUser;
        }

        private List<User> GetUsers()
        {
            var users = new List<User>();
            var reader = this._storeServices.ReadUsersFromFile();
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var currentUserType = (UserType) Enum.Parse(typeof(UserType), line.Split(',')[4].ToString());
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = currentUserType,
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                users.Add(user);
            }

            reader.Close();

            return users;
        }

     
    }
}