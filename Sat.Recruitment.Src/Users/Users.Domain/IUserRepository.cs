namespace Users.Domain
{
    public interface IUserRepository
    {
        void save(User user);

        Task<User?> Search(User user);
    }
}
