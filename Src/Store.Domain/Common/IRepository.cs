namespace Store.Domain.Common
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        Task CreateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
        Task<TEntity?> GetAsync(long id);
        Task<List<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetQueryable();
    }
}
