using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Enums;
using Sat.Recruitment.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Sat.Recruitment.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAll(Func<User, bool> filter = null)
        {
            List<User> users = new List<User>();

            #region Get users from file

            string path = AppContext.BaseDirectory + "/Files/Users.txt";
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (reader.Peek() >= 0)
                {
                    // Read a line
                    var line = reader.ReadLineAsync().Result;

                    // Split a line by the , symbol separator
                    string[] lineColumns = line.Split(new char[] { ',' });

                    if (lineColumns.Length != 6)
                    {
                        throw new CorruptRegistryException(line, $"Amount of columns: {lineColumns.Length}");
                    }

                    // Pass each row column to a corresponding field name
                    string name = lineColumns[0];
                    string email = lineColumns[1];
                    string phone = lineColumns[2];
                    string address = lineColumns[3];
                    string userType = lineColumns[4];
                    string money = lineColumns[5];

                    // Fiel a User entity with the corresponding fields, and do the corresponding conversion in case is needed
                    var user = new User();
                    try
                    {
                        // TODO: Throw specific exceptions for each field that is parsed (Email with a correct pattern,
                        // UserType with an integer belonging to the Enum, etc).

                        user.Name = name;
                        user.Email = email;
                        user.Phone = phone;
                        user.Address = address;
                        user.UserType = (string.IsNullOrEmpty(userType)) ? null : (UserType?)Int32.Parse(userType);
                        user.Money = decimal.Parse(money);
                    }
                    catch (Exception)
                    {
                        throw new CorruptRegistryException(line, "Error occurred in data parsing.");
                    }

                    users.Add(user);
                }
            }

            #endregion // Get users from file

            // If filter parameter is not null, apply it to the results
            if (filter != null)
            {
                users = users.Where(filter).ToList();
            }

            return users;
        }
    }
}
