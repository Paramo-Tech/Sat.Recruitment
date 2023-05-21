using Microsoft.Extensions.Options;
using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Respositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Sat.Recruitment.Infrastructure.Data
{
    public class XMLRepository : IUserRepository
    {
        private readonly FilePathsConfiguration _filePathsConfiguration;

        public XMLRepository(IOptions<FilePathsConfiguration> filePathsConfiguration)
        {
            _filePathsConfiguration = filePathsConfiguration.Value;
        }

        public void CreateUser(User user)
        {
            var users = ReadUsers();
            users.Add(user);
            WriteUsers(users);
        }

        public List<User> ReadUsers()
        {
            var users = new List<User>();

            var xDocument = XDocument.Load(_filePathsConfiguration.UsersXmlFile);
            var userElements = xDocument.Root.Elements("User");

            foreach (var userElement in userElements)
            {
                var name = userElement.Element("Name")?.Value;
                var email = userElement.Element("Email")?.Value;
                var phone = userElement.Element("Phone")?.Value;
                var address = userElement.Element("Address")?.Value;
                var userType = (UserType)Enum.Parse(typeof(UserType), userElement.Element("UserType")?.Value);
                var money = decimal.Parse(userElement.Element("Money")?.Value);

                var user = new User
                {
                    Name = name,
                    Email = email,
                    Phone = phone,
                    Address = address,
                    UserType = userType,
                    Money = money
                };

                users.Add(user);
            }

            return users;
        }

        private void WriteUsers(List<User> users)
        {
            var xDocument = new XDocument(new XElement("Users"));

            foreach (var user in users)
            {
                var userElement = new XElement("User",
                    new XElement("Name", user.Name),
                    new XElement("Email", user.Email),
                    new XElement("Phone", user.Phone),
                    new XElement("Address", user.Address),
                    new XElement("UserType", user.UserType),
                    new XElement("Money", user.Money)
                );

                xDocument.Root.Add(userElement);
            }

            xDocument.Save(_filePathsConfiguration.UsersXmlFile);
        }
    }
}