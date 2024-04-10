using OrleansDemo.Common.Domain;

namespace OrleansDemo.Common.Infrastructure;

public interface IRepository<TEntity> where TEntity : BaseEntity, IAggregateRoot
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AddRangeAsync(List<TEntity> entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    TEntity Update(TEntity entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    bool Remove(TEntity entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(TEntity entity);
}

public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : BaseIdEntity<TKey>, IAggregateRoot
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> WhereAll();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> FirstByIdAsync(TKey Id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="navigationPropertyPaths"></param>
    /// <returns></returns>
    Task<TEntity> FirstByIdAsync(TKey Id, CancellationToken cancellationToken = default,
        params string[] navigationPropertyPaths);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    TEntity FirstById(TKey Id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool Delete(TKey id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="navigationPropertyPaths"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default,
        params string[] navigationPropertyPaths);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    TEntity Get(TKey id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
}