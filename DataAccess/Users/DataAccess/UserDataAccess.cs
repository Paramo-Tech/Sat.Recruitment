using Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        public IEnumerable<User> GetUsers()
        {
            var users = new List<User>();
            using (var readerUser = new FileReaderUser().GetStreamReaderUser())
            {
                string line;
                while ((line = readerUser.ReadLine()) != null)
                {
                    var lineSeparated = line.Split(',');
                    var userToAdd = new User
                    {
                        Name = lineSeparated[0],
                        Email = lineSeparated[1],
                        Phone = lineSeparated[2],
                        Address = lineSeparated[3],
                        UserType = lineSeparated[4],
                        Money = decimal.Parse(lineSeparated[5]),
                    };
                    users.Add(userToAdd);
                }
            }
            return users;
        }

    }
}
