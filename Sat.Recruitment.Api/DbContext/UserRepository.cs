using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.DbContext
{
    public class UserRepository : IRepository<User>
    {
        private readonly string _filePath;

        public UserRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                var user = UserFactory.CreateUser(parts[4]);
                user.Name = parts[0];
                user.Email = parts[1];
                user.Phone = parts[2];
                user.Address = parts[3];
                user.UserType = (UserType)Enum.Parse(typeof(UserType), parts[4]); //parts[4];
                user.Money = Convert.ToDecimal(parts[5]);
                users.Add(user);
            }
            return users;
        }

        public User GetById(int id)
        {
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                var userId = int.Parse(parts[0]);
                if (userId == id)
                {
                    var user = UserFactory.CreateUser(parts[4]);
                    user.Name = parts[0];
                    user.Email = parts[1];
                    user.Phone = parts[2];
                    user.Address = parts[3];
                    user.UserType = (UserType)Enum.Parse(typeof(UserType), parts[4]); //parts[4];
                    user.Money = Convert.ToDecimal(parts[5]);
                    return user;
                }
            }
            return null;
        }

        public void Add(User entity)
        {
            var users = GetAll().ToList();
            //this model hasn´t id in file repo
            //var maxId = users.Count > 0 ? users.Max(c => c.Id) : 0;
            //entity.Id = maxId + 1;

            // add the new user at the list
            users.Add(entity);

            using (var writer = new StreamWriter(_filePath))
            {
                foreach (var user in users)
                {
                    writer.WriteLineAsync($"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType.ToString()},{user.Money.ToString()}");
                }
            }
        }

        public void Delete(User entity)
        {
            var users = GetAll().ToList();
            users.RemoveAll(c => c.Id == entity.Id);
            var lines = users.Select(c => $"{c.Id},{c.Name},{c.Email}");
            File.WriteAllLines(_filePath, lines);
        }

        public async Task SaveChangesAsync()
        {
            await Task.CompletedTask;
        }

        public bool Exists(User user)
        {
            var users = GetAll();

            if (users.Any(u => u.Email == user.Email || u.Phone == user.Phone))
                return true;

            if (users.Any(u => u.Name == user.Name && u.Address == user.Address))
                return true;

            return false;
        }
    }

}
