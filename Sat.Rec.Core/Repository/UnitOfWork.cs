using Sat.Rec.Core.Infrastructure;
using Sat.Rec.Core.Repository.Interfaces;

namespace Sat.Rec.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbUsersContext _dbContext;
        public IUserRepository Users { get; }
        public IUserTypeRepository UserTypes { get; }
        public IGIFUserTypeRepository GIFUserTypes { get; }

        public UnitOfWork(DbUsersContext dbContext,
                            IUserRepository users,
                            IUserTypeRepository userTypes,
                            IGIFUserTypeRepository gifUserTypeRepository)
        {
            _dbContext = dbContext;
            Users = users;
            UserTypes = userTypes;
            GIFUserTypes = gifUserTypeRepository;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
