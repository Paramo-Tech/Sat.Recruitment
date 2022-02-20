using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Enums;
using Sat.Recruitment.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _path;

        public UserRepository()
        {
            _path = AppContext.BaseDirectory + "/Files/Users.txt";
        }

        public async Task<List<User>> GetAll(Func<User, bool> filter = null)
        {
            List<User> users = new List<User>();

            // Get users from file
            using (FileStream fileStream = new FileStream(_path, FileMode.Open))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (reader.Peek() >= 0)
                {
                    // Read a line
                    string line = await reader.ReadLineAsync();

                    // Map line to User
                    User user = MapFileRowToUser(line);

                    users.Add(user);
                }
            }

            // If filter parameter is not null, apply it to the results
            if (filter != null)
            {
                users = users.Where(filter).ToList();
            }

            return users;
        }

        public async Task<User> Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Create Id for the new user
            user.Id = Guid.NewGuid();

            // Add user to file
            using (StreamWriter writer = new StreamWriter(_path, append: true))
            {
                // Map User to Line
                string line = MapUserToFileRow(user);

                // Write line
                await writer.WriteAsync(Environment.NewLine + line);
            }

            return user;
        }

        /// <summary>
        /// Given a file row, fill each of a User entity properties with each
        /// corresponding column of the row.
        /// </summary>
        private User MapFileRowToUser(string fileRow)
        {
            // Split a line by the , symbol separator
            string[] lineColumns = fileRow.Split(new char[] { ',' });

            if (lineColumns.Length != 7)
            {
                throw new CorruptRegistryException(fileRow, $"Amount of columns: {lineColumns.Length}");
            }

            // Pass each row column to a corresponding field name
            string id = lineColumns[0];
            string name = lineColumns[1];
            string email = lineColumns[2];
            string phone = lineColumns[3];
            string address = lineColumns[4];
            string userType = lineColumns[5];
            string money = lineColumns[6];

            // Fill a User entity with the corresponding fields, and do the corresponding conversion in case is needed
            var user = new User();
            try
            {
                // TODO: Throw specific exceptions for each field that is parsed (Email with a correct pattern,
                // UserType with an integer belonging to the Enum, etc).

                user.Id = Guid.Parse(id);
                user.Name = name;
                user.Email = email;
                user.Phone = phone;
                user.Address = address;
                user.UserType = (string.IsNullOrEmpty(userType)) ? null : (UserType?)Int32.Parse(userType);
                user.Money = decimal.Parse(money);
            }
            catch (Exception)
            {
                throw new CorruptRegistryException(fileRow, "Error occurred in data parsing.");
            }

            return user;
        }

        /// <summary>
        /// Given a User, transform each of its properties to the columns of a file row.
        /// </summary>
        private string MapUserToFileRow(User user)
        {
            // Transform the UserType to proper form to be stored in a text file
            string userType = (user.UserType == null) ? String.Empty : ((int)user.UserType).ToString();

            string fileRow = $"{user.Id},{user.Name},{user.Email},{user.Phone},{user.Address},{userType},{user.Money}";

            return fileRow;
        }
    }
}
