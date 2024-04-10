namespace OrleansDemo.Common.Domain;

public class BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    public  StatusEnum Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public  DateTime Created { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public  DateTime? Updated { get; set; }
}