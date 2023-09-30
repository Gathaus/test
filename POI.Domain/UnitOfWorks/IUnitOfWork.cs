
using Microsoft.EntityFrameworkCore.Storage;
using POI.Domain.Repositories;

namespace POI.Domain.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IRepository<T, int> Repository<T>() where T : class;

        public Task<IDbContextTransaction> BeginTransactionAsync();

        public Task CommitAsync();

        public Task RollbackAsync();
        
        public Task<int> SaveChangesAsync();
    }
}
