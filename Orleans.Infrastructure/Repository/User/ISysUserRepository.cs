using Orleans.Domain.Entity.UserAggregate;
using OrleansDemo.Common.Infrastructure;

namespace Orleans.Infrastructure.Repository.User;

public interface ISysUserRepository : IRepository<SysUser, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<SysUser> GetUserByAccount(string Account);

    /// <summary>
    ///     
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    Task<SysUser> GetUserByUserId(string UserId);
}
