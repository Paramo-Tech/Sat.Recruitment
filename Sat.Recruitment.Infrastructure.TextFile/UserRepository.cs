using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Enum;
using Sat.Recruitment.Domain.Model;
using Sat.Recruitment.Infrastructure.Exceptions;
using Sat.Recruitment.Infrastructure.TextFile.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.TextFile
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly ITextFileConfiguration _textFileConfiguration;

        public UserRepository(
            ILogger<UserRepository> logger,
            ITextFileConfiguration textFileConfiguration)
        {
            _textFileConfiguration = textFileConfiguration;
            _logger = logger;
        }

        public async Task<User> AddAsync(User entity)
        {
            _logger.LogDebug("Adding user {entity} to text file.", entity);            

            try
            {
                using (StreamWriter usersFile = new StreamWriter(_textFileConfiguration.TextFilePath(), true))
                {
                    var line = TextFileUtils.UserToFileLine(entity);
                    await usersFile.WriteLineAsync($"{Environment.NewLine}{line}");
                }

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add user {userName}.", entity.Name);
                throw new RepositoryException(ex.Message);
            }
        }

        public IEnumerable<User> GetAll()
        {
            _logger.LogDebug("Getting all users from text file.");
            try
            { 
                var users = new List<User>();
                using (StreamReader reader = new StreamReader(new FileStream(_textFileConfiguration.TextFilePath(), FileMode.Open)))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLine();
                        var user = MapFromFileLineToUser(line);
                        users.Add(user);
                    }
                }

                return users;
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to retrieve all users.");
                throw new RepositoryException(ex.Message);
            }
        }

        private User MapFromFileLineToUser(string line)
        {
            Enum.TryParse(TextFileUtils.GetElementFromLine(4, line), out UserType userType);
            var user = new User(TextFileUtils.GetElementFromLine(0, line),
                                TextFileUtils.GetElementFromLine(1, line),
                                TextFileUtils.GetElementFromLine(2, line),
                                TextFileUtils.GetElementFromLine(3, line),
                                userType,
                                decimal.Parse(TextFileUtils.GetElementFromLine(5, line)));            

            return user;
        }
    }
}
