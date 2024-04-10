using System.ComponentModel.DataAnnotations.Schema;

namespace OrleansDemo.Common.Domain;

/// <summary>
///领域实体
/// </summary>
/// <typeparam name="T"> 泛型主键</typeparam>
public class BaseIdEntity<T> : BaseEntity, IAggregateRoot
{
    /// <summary>
    /// 
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public T Id { get; set; }
}