using Shared.Domain;

namespace Users.Domain
{
    public interface IUserRepository
    {
        void save(User user);

        Task<User?> Search(ISpecification<User> specification);
    }
}
