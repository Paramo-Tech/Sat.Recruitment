using Sat.Recruitment.Global.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Sat.Recruitment.Global.WebContracts;

namespace Sat.Recruitment.Services
{
    public class UsersService : IUsersService
    {
        private readonly List<User> _users = new List<User>();

        public List<User> UpdateUserList(User newUser)
        {
            try
            {
                newUser.Money = CalculateAdditional(newUser);

                newUser.Email = Helper.NormalizeEmail(newUser);

                var reader = Helper.ReadUsersFromFile();

                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    var user = new User(line.Split(',')[0].ToString(), line.Split(',')[1].ToString(),
                        line.Split(',')[2].ToString(), line.Split(',')[3].ToString(), line.Split(',')[4].ToString(),
                        line.Split(',')[5].ToString());
                    _users.Add(user);
                }

                reader.Close();

                return _users;
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.Message);
                throw e;
            }
        }

        private decimal CalculateAdditional(User newUser)
        {
            var moneyCalculated = newUser.Money;

            switch (newUser.UserType)
            {
                case "Normal":
                    {
                        if (newUser.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.12);
                            //If new user is normal and has more than USD100
                            var gif = newUser.Money * percentage;
                            moneyCalculated = newUser.Money + gif;
                        }

                        if (newUser.Money > 10 && newUser.Money < 100)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = newUser.Money * percentage;
                            moneyCalculated = newUser.Money + gif;
                        }

                        break;
                    }
                case "SuperUser":
                    {
                        if (newUser.Money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.20);
                            var gif = newUser.Money * percentage;
                            moneyCalculated = newUser.Money + gif;
                        }

                        break;
                    }
                case "Premium":
                    {
                        if (newUser.Money > 100)
                        {
                            var gif = newUser.Money * 2;
                            moneyCalculated = newUser.Money + gif;
                        }

                        break;
                    }
            }

            return moneyCalculated;
        }
    }
}