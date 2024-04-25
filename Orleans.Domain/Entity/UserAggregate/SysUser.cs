using OrleansDemo.Common.Domain;

namespace Orleans.Domain.Entity.UserAggregate;
public partial class SysUser : BaseIdEntity<string>, IAggregateRoot
{
    /// <summary>
    ///     
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    ///     
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string PassWord { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool IsEnable { get; set; }

}
