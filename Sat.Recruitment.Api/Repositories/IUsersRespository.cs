
namespace Sat.Recruitment.Api.Repositories
{
    using Sat.Recruitment.Api.Domain;
    using System.Collections.Generic;

    public interface IUsersRepository
    {
        public bool exists(User user);
        public List<User> get();
        public List<User> get(string name, string email, string address, string phone, string userType, string money);
        public void create(User user);
    }
}
