using Shared.Domain;
using Shared.Domain.Exceptions;
using Users.Domain;

namespace Users.infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private const string SavedUsersFilePath = "/Files/Users.txt";

        public async Task Save(User user)
        {
            using (StreamWriter usersFile = new(GetFilePath(), append: true))
            {
                await usersFile.WriteLineAsync(UserFileMapper.ToLine(user));
            }
        }

        public async Task<User?> Search(ISpecification<User> specification)
        {
            var users = await this.GetAll();
            foreach (var savedUser in users)
            {
                if (specification.IsSatisfied(savedUser))
                {
                    return savedUser;
                }
            }

            return null;
        }

        private async Task<List<User>> GetAll()
        {
            try
            {
                FileStream fileStream = new (GetFilePath(), FileMode.Open);
                StreamReader reader = new (fileStream);

                List<User> users = new();

                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var user = UserFileMapper.ToUser(line);
                    users.Add(user);
                }
                reader.Close();

                return users;
            }
            catch(Exception ex)
            {
                throw new RepositoryException("An error ocurred trying to get users", ex);
            }
        }

        private static string GetFilePath() => Directory.GetCurrentDirectory() + SavedUsersFilePath;
    }
}