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
}
