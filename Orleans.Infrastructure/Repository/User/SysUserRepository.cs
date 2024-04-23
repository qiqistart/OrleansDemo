using Microsoft.EntityFrameworkCore;
using Orleans.Domain.Entity.UserAggregate;
using OrleansDemo.Common.Infrastructure;
using System.Security.Principal;

namespace Orleans.Infrastructure.Repository.User;

/// <summary>
/// 
/// </summary>
public class SysUserRepository : Repository<SysUser, string, OrleansDbContext>, ISysUserRepository
{
    public SysUserRepository(OrleansDbContext context) : base(context)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserByAccount(string Account)
    {
        var userData = await WhereAll().FirstOrDefaultAsync(u => u.Account == Account);
        return userData;
    }

    /// <summary>
    ///    
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserByUserId(string UserId)
    {
        var userData = await WhereAll().FirstOrDefaultAsync(u => u.Id == UserId);
        return userData;
    }
}
