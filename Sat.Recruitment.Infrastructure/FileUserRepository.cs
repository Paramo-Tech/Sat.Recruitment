using System;
using System.Linq;
using System.Numerics;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Aggregates;
using Sat.Recruitment.Domain.ValueObjects;

namespace Sat.Recruitment.Infrastructure
{
    public class FileUserRepository : IUserRepository
    {
        public FileUserRepository()
        {
        }

        public async Task Save(User user)
        {
            //throw new NotImplementedException();
        }

        public async Task<User?> SearchBy(Func<User, bool> predicate)
        {
            var _users = new List<User>();
            var reader = ReadUsersFromFile();
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                if (line == null) continue;
                var newUser = new User
                (
                    new UserName(line.Split(',')[0].ToString()),
                    new Email(line.Split(',')[1].ToString()),
                    new Address(line.Split(',')[3].ToString()),
                    new Phone(line.Split(',')[2].ToString()),
                    new UserType(line.Split(',')[4].ToString()),
                    new Money(decimal.Parse(line.Split(',')[5].ToString()))
                );
                _users.Add(newUser);
            }
            var user = _users.FirstOrDefault(predicate);
            reader.Close();
            return user;
        }

        //public async Task<User?> SearchBy(Email email, Phone phone)
        //{
        //    var _users = new List<User>();
        //    var reader = ReadUsersFromFile();
        //    while (reader.Peek() >= 0)
        //    {
        //        var line = await reader.ReadLineAsync();
        //        if (line == null) continue;
        //        var newUser = new User
        //        (
        //            new UserName(line.Split(',')[0].ToString()),
        //            new Email(line.Split(',')[1].ToString()),
        //            new Address(line.Split(',')[3].ToString()),
        //            new Phone( line.Split(',')[2].ToString()),
        //            new UserType (line.Split(',')[4].ToString()),
        //            new Money (decimal.Parse(line.Split(',')[5].ToString()))
        //        );
        //        _users.Add(newUser);
        //    }
        //    var user = (from u in _users
        //                 where u.Email == email || u.Phone == phone
        //                 select u).FirstOrDefault();
        //    reader.Close();
        //    return user;
        //}

        //public async Task<User?> SearchBy(UserName name, Address address)
        //{
        //    var _users = new List<User>();
        //    var reader = ReadUsersFromFile();
        //    while (reader.Peek() >= 0)
        //    {
        //        var line = await reader.ReadLineAsync();
        //        if (line == null) continue;
        //        var newUser = new User
        //        (
        //            new UserName(line.Split(',')[0].ToString()),
        //            new Email(line.Split(',')[1].ToString()),
        //            new Address(line.Split(',')[3].ToString()),
        //            new Phone(line.Split(',')[2].ToString()),
        //            new UserType(line.Split(',')[4].ToString()),
        //            new Money(decimal.Parse(line.Split(',')[5].ToString()))
        //        );
        //        _users.Add(newUser);
        //    }
        //    var user = (from u in _users
        //                where u.Name == name && u.Address == address
        //                select u).FirstOrDefault();
        //    reader.Close();
        //    return user;
        //}

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}

