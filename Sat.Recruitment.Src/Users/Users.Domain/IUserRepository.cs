using Shared.Domain;

namespace Users.Domain
{
    public interface IUserRepository
    {
        Task Save(User user);

        Task<User?> Search(ISpecification<User> specification);
    }
}
