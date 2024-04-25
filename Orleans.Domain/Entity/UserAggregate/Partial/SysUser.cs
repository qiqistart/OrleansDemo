using OrleansDemo.Common.SnowflakeModule;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orleans.Domain.Entity.UserAggregate;

/// <summary>
/// 分部类：系统用户
/// </summary>
public partial class SysUser
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
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="account"></param>
    /// <param name="avatar"></param>
    public SysUser(string userName, string passWord, string account, string avatar)
    {
        Id = SnowflakeHelper.NewId();
        UserName = userName;
        PassWord = passWord;
        Account = account;
        Avatar = avatar;
        IsEnable = true;
        Created = DateTime.Now;
    }

    #region  导航属性

    /// <summary>
    /// 
    /// </summary>
    [ForeignKey("Id")]
    public virtual SysUserConfig SysUserConfig { get; set; }
    #endregion
}