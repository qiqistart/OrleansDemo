using Orleans.Domain.Entity;
using OrleansDemo.Common.Infrastructure;



namespace Orleans.Infrastructure.Repository.User;

/// <summary>
/// 
/// </summary>
public class SysUserRepository : Repository<SysUser, string, OrleansDbContext>, ISysUserRepository
{
    public SysUserRepository(OrleansDbContext context) : base(context)
    {
    }
}
