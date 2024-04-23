using OrleansDemo.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Domain.Entity.UserAggregate;
public class SysUser : BaseIdEntity<string>, IAggregateRoot
{
    /// <summary>
    ///             
    /// </summary>
    public SysUser()
    {

    }
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
