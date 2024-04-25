using OrleansDemo.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Domain.Entity.UserAggregate;

/// <summary>
/// 
/// </summary>
public partial class SysUserConfig : BaseIdEntity<string>
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// 账户余额
    /// </summary>
    public decimal AccountBalance { get; set; }

    /// <summary>
    /// 是否开启透支消费
    /// </summary>
    public bool IsOpenOverdraftConsumption { get; set; }
    /// <summary>
    /// 透支消费余额
    /// </summary>

    public decimal OverdraftConsumptionBalance { get; set; }
}
