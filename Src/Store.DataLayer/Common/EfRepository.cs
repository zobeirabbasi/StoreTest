using Microsoft.EntityFrameworkCore;
using Store.Domain.Common;

namespace Store.DataLayer.Common
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        ApplicationContext _dbcontext;
        public EfRepository(ApplicationContext context)
        {
            _dbcontext = context;
        }
        public Task<TEntity?> GetAsync(long id)
        {
            return _dbcontext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _dbcontext.Set<TEntity>().ToListAsync();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbcontext.Set<TEntity>().AsQueryable();
        }

        public Task CreateAsync(TEntity entity)
        {
            _dbcontext.AddAsync(entity);
            return _dbcontext.SaveChangesAsync();
        }

        public Task RemoveAsync(TEntity entity)
        {
            _dbcontext.Remove(entity);
            return _dbcontext.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            return _dbcontext.SaveChangesAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            //  _dbcontext.Entry(entity).State = EntityState.Modified;
            _dbcontext.Update(entity);
            return _dbcontext.SaveChangesAsync();
        }
    }
}
