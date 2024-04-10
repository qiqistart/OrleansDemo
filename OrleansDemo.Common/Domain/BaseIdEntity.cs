using System.ComponentModel.DataAnnotations.Schema;

namespace OrleansDemo.Common.Domain;

/// <summary>
///领域实体
/// </summary>
/// <typeparam name="T"> 泛型主键</typeparam>
public abstract class BaseIdEntity<T> : BaseEntity, IEntity<T>
{
    /// <summary>
    /// 
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual T Id { get; set; }
}
