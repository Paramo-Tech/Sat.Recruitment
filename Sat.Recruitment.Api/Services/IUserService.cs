namespace Sat.Recruitment.Api.Services
{
    using Sat.Recruitment.Api.Domain;
    using System.Collections.Generic;

    public interface IUserService
    {

        //public void createUser(string name, string email, string address, string phone, string userType, string money);

        public void createUser(User newUser);

        public List<User> getUsers(string name, string email, string address, string phone, string userType, string money);

    }
}
