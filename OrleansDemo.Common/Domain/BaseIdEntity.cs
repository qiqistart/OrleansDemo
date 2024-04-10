using System.ComponentModel.DataAnnotations.Schema;

namespace OrleansDemo.Common.Domain;

/// <summary>
/// 泛型主键
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseIdEntity<T> : BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public T Id { get; set; }
}