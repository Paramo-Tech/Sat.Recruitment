using Sat.Recruitment.DAL.Interfaces;
using Sat.Recruitment.DAL.models;
using Sat.Recruitment.DAL.utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly string _filePath;
        private readonly char SEPARATOR = ',';
        public UserRepository()
        {
            _filePath = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        }

        public async void Create(User entry)
        {
            var users = (await Get()).ToList();
            users.Add(entry);

            using(var writter = new StreamWriter(_filePath))
            {
                foreach(var user in users) {
                    await writter.WriteLineAsync(
                        $"{user.Name}," +
                        $"{user.Email}," +
                        $"{user.Phone}," +
                        $"{user.Address}," +
                        $"{Helper.Capitalize(user.UserType)}," +
                        $"{user.Money}"
                    );
                }
            }
        }

        public async void Delete(User entry)
        {
            var users = (await Get()).ToList();
            users.Remove(entry);
            var lines = users.Select(u => $"{u.Name},{u.Email},{u.Phone},{u.Address},{u.UserType},{u.Money}");
            File.WriteAllLines(_filePath, lines);
        }

        public async Task<IEnumerable<User>> Get()
        {
            var users = new List<User>();
            var lines = await File.ReadAllLinesAsync(_filePath);
            foreach (var line in lines){
                var fields = line.Split(SEPARATOR);
                var user = new User { 
                    Name = fields[0],
                    Email = fields[1],
                    Phone = fields[2],
                    Address = fields[3],
                    UserType = fields[4],
                    Money = decimal.Parse(fields[5])
                };
                users.Add(user);
            }
            return users;
        }

        public async Task<bool> Find(User entry)
        {
            var users = (await Get()).ToList();
            var finded = users.Where(x => (x.Name == entry.Name && x.Address == entry.Address) || (x.Name == entry.Name || x.Phone == entry.Phone));

            return finded.Count() > 0; 
        }

    }
}
