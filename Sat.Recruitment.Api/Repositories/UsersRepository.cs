namespace Sat.Recruitment.Api.Repositories
{
    using System.Collections.Generic;
    using Sat.Recruitment.Api.Domain;
    using System.IO;
    using System;


    public class UsersRepository : IUsersRepository
    {
        private string path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        private StreamReader ReadUsersFromFile()
        {

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        private StreamWriter WriteUsersToFile()
        {

            FileStream fileStream = new FileStream(path, FileMode.Append);

            StreamWriter writer = new StreamWriter(fileStream);
            return writer;
        }

        public List<User> get()
        {
            var reader = ReadUsersFromFile();
            List<User> users = new List<User>();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                if (!string.IsNullOrEmpty(line))
                {
                    var lineFields = line.Split(',');

                    var user = new User
                    {
                        Name = lineFields[0].ToString(),
                        Email = lineFields[1].ToString(),
                        Phone = lineFields[2].ToString(),
                        Address = lineFields[3].ToString(),
                        UserType = lineFields[4].ToString(),
                        Money = decimal.Parse(lineFields[5].ToString()),
                    };
                    users.Add(user);
                }
            }
            reader.Close();
            return users;
        }

        public bool exists(User user)
        {
            var users = this.get();
            var isDuplicated = users.Exists(u =>
                (u.Email == user.Email || u.Phone == user.Phone) ||
                u.Name == user.Name && u.Address == user.Address);
            return isDuplicated;
        }

        public void create(User user)
        {
            var writer = WriteUsersToFile();
            writer.WriteLine(user.Name + "," + user.Email + "," + user.Phone + "," + user.Address + "," + user.UserType + "," + user.Money);
            writer.Close();
        }

        public List<User> get(string name, string email, string address, string phone, string userType, string money)
        {
            //use parameters to filter results
            throw new NotImplementedException();
        }

    }
}
