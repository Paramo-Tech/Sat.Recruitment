namespace Sat.Rec.Core.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IUserTypeRepository UserTypes { get; }
        IGIFUserTypeRepository GIFUserTypes { get; }

        int Save();
    }
}
