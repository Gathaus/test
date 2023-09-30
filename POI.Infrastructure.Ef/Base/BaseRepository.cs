using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using POI.Domain.Entities;
using POI.Domain.Repositories;
using POI.Infrastructure.Ef.Context;
using POI.Infrastructure.Ef.Extensions;

namespace POI.Infrastructure.Ef.Base
{
    public class BaseRepository<TEntity> : BaseRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        #region Constructors

        public BaseRepository(PoiDbContext context)
            : base(context)
        {

        } 
        
        #endregion
    }

public class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entitySet;

        public BaseRepository(PoiDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entitySet = _context.Set<TEntity>();
        }

        // CREATE operations
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _entitySet.AddAsync(entity);
            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            _entitySet.Add(entity);
            return entity;
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entitySet.AddRangeAsync(entities);
        }

        public async Task BulkInsertAsync(IEnumerable<TEntity> entities)
        {
            await _context.BulkInsertAsync(entities.ToList());
        }

        // READ operations
        public async Task<TEntity> GetByIdAsync(TPrimaryKey id, params string[] includeParams)
        {
            return await _baseQuery(includeParams).FirstOrDefaultAsync(q => q.Id.Equals(id)) ?? throw new InvalidOperationException();
        }

        public async Task<TEntity> GetAsync(params string[] includeParams)
        {
            return await _baseQuery(includeParams).FirstOrDefaultAsync() ?? throw new InvalidOperationException();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, params string[] includeParams)
        {
            return await _baseQuery(includeParams).FirstOrDefaultAsync(expression) ?? throw new InvalidOperationException();
        }

        public IQueryable<TEntity> FindBy(params string[] includeParams)
        {
            return _baseQuery(includeParams);
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression, params string[] includeParams)
        {
            return _baseQuery(includeParams).Where(expression);
        }
        
        public IQueryable<TEntity> QueryFromSql(string sql, params object[] parameters)
        {
            return _entitySet.FromSqlRaw(sql, parameters).AsQueryable();
        }
        
        



        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public bool Delete(TEntity entity)
        {
            if (entity == null) return false;

            // If you have a soft delete mechanism
            // entity.IsDeleted = true;
            Update(entity);
            return true;
        }

        public bool HardDelete(TEntity entity)
        {
            if (entity == null) return false;

            _context.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public async Task BulkDeleteAsync(IEnumerable<TEntity> entities)
        {
            await _context.BulkDeleteAsync(entities.ToList());
        }

        private IQueryable<TEntity> _baseQuery(params string[] includeParams)
        {
            return _entitySet.IncludeAll(includeParams);
        }
    }

}
