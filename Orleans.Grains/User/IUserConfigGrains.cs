using Orleans.Domain.Entity.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Grains.User;
/// <summary>
/// 
/// </summary>
public interface IUserConfigGrains: IGrainWithStringKey
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    Task<SysUserConfig> GetUserConfig(string UserId);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userConfig"></param>
    /// <returns></returns>
    /// 
    [Transaction(TransactionOption.Join)]
    Task<bool> AddUserConfig(SysUserConfig userConfig);
}
