
using Sat.Recruitment.Domain.Contracts.Repositories;
using Sat.Recruitment.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    /// <summary>
    /// Mocking Repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private string _fileName = "/Files/Users.txt";

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        public async Task<List<User>> GetUsers()
        {
            List<User> users = new List<User>();
            var path = Directory.GetCurrentDirectory() + FileName;
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
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

            }
            return users;
        }

        public string ConvertirUsuarioToString(User user)
        {
            return string.Format("{0},{1},{2},{3},{4},{5}", user.Name, user.Email, user.Phone, user.Address, user.UserType, user.Money);

        }
        public async Task AddUser(User user)
        {
            await _semaphore.WaitAsync();
            {
                try
                {

                    using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "/Files/Users.txt", true))
                    {
                        await writer.WriteLineAsync();
                        await writer.WriteAsync(ConvertirUsuarioToString(user));
                        writer.Close();

                    }
                }
                catch (Exception err)
                {
                    throw err;
                }

                finally
                {
                    _semaphore.Release();
                }
            }
        }
    }
}
