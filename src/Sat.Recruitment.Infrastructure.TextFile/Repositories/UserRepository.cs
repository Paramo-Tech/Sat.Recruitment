using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Enums;
using Sat.Recruitment.Infrastructure.TextFile.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Sat.Recruitment.Infrastructure.TextFile.Repositories
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

        /// <summary>
        /// Returns the User corresponding to the Id received by parameter,
        /// or null if the user does not exist.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CorruptStorageException"></exception>
        public async Task<User> GetById(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            List<User> users = await this.GetAll(u => u.Id == id);

            if (users.Count > 1)
            {
                throw new CorruptStorageException($"When searching for a User by its Id -which is unique-, more than one entity was found. Id: {id}");
            }

            return users.FirstOrDefault();
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

        public async Task Delete(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            // Get all the lines from the file
            List<string> lines = (await File.ReadAllLinesAsync(_path)).ToList();

            // Search the corresponding line to the User to be deleted
            for (int i = 0; i < lines.Count; i++)
            {
                // Get a line from the file
                string line = lines[i];

                // Map line to User
                User currentUser = MapFileRowToUser(line);

                // If the Id of the current User matches with the Id of the User to be deleted,
                // then, remove the current line and end the search.
                if (currentUser.Id == id)
                {
                    lines.RemoveAt(i);
                    break;
                }
            }

            // Write the remaining lines to the file
            await File.WriteAllLinesAsync(_path, lines.ToArray());
        }

        public async Task<User> Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // User to be returned after all the process
            User updatedUser = null;

            // Get all the lines from the file
            List<string> lines = (await File.ReadAllLinesAsync(_path)).ToList();

            // Search the corresponding line to the User to be updated
            for (int i = 0; i < lines.Count; i++)
            {
                // Get a line from the file
                string line = lines[i];

                // Map line to User
                User currentUser = MapFileRowToUser(line);

                // If the Id of the current User matches with the Id of the User to be updated,
                // then, update the current line and end the search.
                if (currentUser.Id == user.Id)
                {
                    // Update the fields of the persisted entity, with the new fields
                    // received by parameter
                    currentUser.Name = user.Name;
                    currentUser.Email = user.Email;
                    currentUser.Address = user.Address;
                    currentUser.Phone = user.Phone;
                    currentUser.UserType = user.UserType;
                    currentUser.Money = user.Money;

                    // Update the line
                    lines[i] = MapUserToFileRow(currentUser);

                    // Entity to be returned
                    updatedUser = currentUser;

                    break;
                }
            }

            // Write the lines to the file
            await File.WriteAllLinesAsync(_path, lines.ToArray());

            return updatedUser;
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
                user.Money = decimal.Parse(money, CultureInfo.InvariantCulture);
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

            string fileRow = $"{user.Id},{user.Name},{user.Email},{user.Phone},{user.Address},{userType},{user.Money.ToString(CultureInfo.InvariantCulture)}";

            return fileRow;
        }
    }
}
