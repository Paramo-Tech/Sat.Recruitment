using Users.Domain;

namespace Users.infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        public void save(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> Search(User user)
        {
            var users = await this.GetAllAsync();
            foreach (var saveUser in users)
            {
                if (saveUser.Email.Equals(user.Email) || saveUser.Phone.Equals(user.Phone))
                {
                    return saveUser;
                }
                else if (saveUser.Name.Equals(user.Name) && saveUser.Address.Equals(user.Address))
                {
                    return saveUser;
                }
            }

            return null;
        }

        private async Task<List<User>> GetAllAsync()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            FileStream fileStream = new(path, FileMode.Open);
            StreamReader reader = new(fileStream);

            List<User> users = new ();

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var user = UserFileMapper.Execute(line);
                users.Add(user);
            }
            reader.Close();

            return users;
        }
    }
}