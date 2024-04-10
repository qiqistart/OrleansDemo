using Microsoft.EntityFrameworkCore;
using OrleansDemo.Common.Domain;

namespace OrleansDemo.Common.Infrastructure;

public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : BaseEntity, IAggregateRoot
    where TDbContext : DbContext
{
    protected virtual TDbContext DbContext { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    protected Repository(TDbContext context)
    {
        this.DbContext = context;
    }

    public virtual TEntity Add(TEntity entity)
    {
        return DbContext.Add(entity).Entity;
    }

    public virtual async Task AddRangeAsync(List<TEntity> entity)
    {
        await DbContext.AddRangeAsync(entity);
    }

    public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Add(entity));
    }


    public virtual TEntity Update(TEntity entity)
    {
        return DbContext.Update(entity).Entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Update(entity));
    }


    public virtual bool Remove(TEntity entity)
    {
        DbContext.Remove(entity);
        return true;
    }

    public virtual Task<bool> RemoveAsync(TEntity entity)
    {
        return Task.FromResult(Remove(entity));
    }
}

public abstract class Repository<TEntity, TKey, TDbContext> : Repository<TEntity, TDbContext>,
    IRepository<TEntity, TKey> where TEntity : BaseIdEntity<TKey>, IAggregateRoot where TDbContext : DbContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    protected Repository(TDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Where 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> WhereAll()
    {
        var entity = DbContext.Set<TEntity>();
        return entity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual bool Delete(TKey id)
    {
        var entity = DbContext.Set<TEntity>().FirstOrDefault(m => m.Id.Equals(id));
        if (entity == null)
        {
            return false;
        }

        DbContext.Remove(entity);
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="navigationPropertyPaths"></param>
    /// <returns></returns>
    public virtual async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default,
        params string[] navigationPropertyPaths)
    {
        var entitys = DbContext.Set<TEntity>().AsQueryable();
        foreach (var navigationPropertyPath in navigationPropertyPaths)
        {
            entitys = entitys.Include(navigationPropertyPath);
        }

        var entity = await entitys.FirstOrDefaultAsync(m => m.Id.Equals(id), cancellationToken);
        DbContext.Remove(entity);
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public TEntity FirstById(TKey Id)
    {
        var entity = DbContext.Set<TEntity>().FirstOrDefault(m => m.Id.Equals(Id));
        return entity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public virtual async Task<TEntity> FirstByIdAsync(TKey Id, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(m => m.Id.Equals(Id));
        if (entity == null)
        {
            throw new("数据不存在");
        }

        return entity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="navigationPropertyPaths"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> FirstByIdAsync(TKey Id, CancellationToken cancellationToken = default,
        params string[] navigationPropertyPaths)
    {
        var entity = DbContext.Set<TEntity>().AsQueryable();

        foreach (var navigationPropertyPath in navigationPropertyPaths)
        {
            entity = entity.Include(navigationPropertyPath);
        }

        var result = await entity.FirstOrDefaultAsync(m => m.Id.Equals(Id));
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual TEntity Get(TKey id)
    {
        return DbContext.Find<TEntity>(id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await DbContext.FindAsync<TEntity>(id, cancellationToken);
    }
}