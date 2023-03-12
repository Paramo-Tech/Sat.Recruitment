using Newtonsoft.Json;
using Sat.Recruitment.Global.Interfaces;
using Sat.Recruitment.Global.WebContracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services
{
    public class UsersService : IUsersService
    {
        private readonly IConnectionMultiplexer _redis;

        public UsersService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                var db = _redis.GetDatabase();

                // Implemented Cache to avoid reading the file every time
                HashEntry[] hashUsers = await db.HashGetAllAsync("users");

                if (hashUsers.Length == 0)
                {
                    var users = new List<User>();

                    var reader = Helper.ReadUsersFromFile();

                    while (reader.Peek() >= 0)
                    {
                        var line = await reader.ReadLineAsync();
                        var user = new User(line.Split(',')[0].ToString(), line.Split(',')[1].ToString(),
                            line.Split(',')[2].ToString(), line.Split(',')[3].ToString(), line.Split(',')[4].ToString(),
                            line.Split(',')[5].ToString());
                        users.Add(user);
                    }

                    reader.Close();

                    await db.HashSetAsync("users", users.Select(user => new HashEntry(user.Name, JsonConvert.SerializeObject(user))).ToArray());

                    return users;
                }

                hashUsers = await db.HashGetAllAsync("users");

                return hashUsers.Select(user => JsonConvert.DeserializeObject<User>(user.Value.ToString())).ToList();
            }
            catch (AggregateException e)
            {
                Debug.WriteLine(e.Message);
                throw e;
            }
        }

        public User ProcessUser(User newUser)
        {
            try
            {
                newUser.Money = CalculateAdditional(newUser);

                newUser.Email = Helper.NormalizeEmail(newUser);

                return newUser;
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