using System.ComponentModel.DataAnnotations.Schema;

namespace OrleansDemo.Common.Domain;

/// <summary>
/// 基类实体
/// </summary>
public abstract class BaseEntity:IEntity
{
    /// <summary>
    /// 
    /// </summary>
    public StatusEnum Status { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? Updated { get; set; }
}

