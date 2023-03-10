using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<User> Insert(User User);

        Task<User?> GetUserByUniqueFields(string email, string phone, string name, string address);

        Task<bool> ValidateCredentials(string email, string password);

        Task<string> GetUserTypeByEmail(string email);

    }
}

