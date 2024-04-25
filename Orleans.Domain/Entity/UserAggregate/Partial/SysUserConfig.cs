using OrleansDemo.Common.SnowflakeModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Domain.Entity.UserAggregate;
public partial class SysUserConfig
{
    /// <summary>
    /// 
    /// </summary>
    public SysUserConfig()
    {



    }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountBalance"></param>
    /// <param name="sOpenOverdraftConsumption"></param>
    /// <param name="overdraftConsumptionBalance"></param>
    public SysUserConfig(string userId, decimal accountBalance, bool sOpenOverdraftConsumption, decimal overdraftConsumptionBalance)
    {
        Id = SnowflakeHelper.NewId();
        UserId = userId;
        AccountBalance = accountBalance;
        IsOpenOverdraftConsumption = sOpenOverdraftConsumption;
        OverdraftConsumptionBalance = overdraftConsumptionBalance;
        Created = DateTime.Now;
    }


    #region  导航属性

    /// <summary>
    /// 
    /// </summary>
    [ForeignKey("UserId")]
    public virtual SysUser SysUser { get; set; }
    #endregion
}
