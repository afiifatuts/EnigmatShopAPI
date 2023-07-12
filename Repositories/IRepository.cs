using System.Linq.Expressions;

namespace EnigmaShopApi.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> SaveAsync(TEntity entity);//yang ada TaskNya bisa pakai Async
    TEntity Attach(TEntity entity);
    Task<TEntity?> FindByIdAsync(Guid id);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria, string[] includes);
    Task<List<TEntity>> FindAllAsync();
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria);
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);

}