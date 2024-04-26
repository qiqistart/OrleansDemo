using Orleans.Domain.Entity.UserAggregate;

namespace Orleans.Grains.User;

public interface IUserGrains : IGrainWithStringKey
{
    /// <summary>
    /// 根据账号获取用户信息
    /// </summary>
    /// <param name="Account"></param>
    /// <returns></returns>
    Task<SysUser> GetUserByAccount(string Account);

    /// <summary>
    /// 根据用户Id获取用户信息
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    Task<SysUser> GetUserByUserId(string UserId);

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [Transaction(TransactionOption.Create)]
    Task<bool> AddUser(SysUser user);
}
