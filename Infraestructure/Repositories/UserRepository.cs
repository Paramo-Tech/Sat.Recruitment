using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.CQS.Commands;
using Core.Entities;
using Core.Interfaces;

namespace Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        #region Cache the data for the example, i could use IMemmoryCache or memory dbcontext, for time reasons i gonna keep it

        static List<User> users = new List<User>();
        static List<User> Users
        {
            get
            {
                if (users.Count == 0) // -- to load first time and save on static
                {
                    users = new List<User>();
                    var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
                    FileStream fileStream = new FileStream(path, FileMode.Open);
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (reader.Peek() >= 0)
                        {
                            var line = reader.ReadLineAsync().Result;
                            var splitLine = line.Split(',');
                            var user = new User
                            {
                                Name = splitLine[0].ToString(),
                                Email = splitLine[1].ToString(),
                                Phone = splitLine[2].ToString(),
                                Address = splitLine[3].ToString(),
                                UserType = splitLine[4].ToString(),
                                Money = decimal.Parse(splitLine[5].ToString()),
                            };
                            users.Add(user);
                        }
                    }
                }

                return users;
            }
        }

        #endregion
        
        public async Task<List<User>> GetAll()
        {
            return Users;
        }

        public async Task<bool> Exists(CreateNewUserDTO newUser)
        {
            foreach (var user in Users)
            {
                if (user.Email == newUser.email || user.Phone == newUser.phone)
                {
                    return true;
                }
                else if (user.Name == newUser.name && user.Address == newUser.address)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task Add(User user)
        {
            // in real enviroments we could await i/o database calls etc.
            users.Add(user);
        }
    }
}
