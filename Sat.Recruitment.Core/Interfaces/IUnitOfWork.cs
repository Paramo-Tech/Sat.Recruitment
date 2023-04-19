using System;

namespace Sat.Recruitment.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        void Commit();
    }
}
