using Orleans.Domain.Entity;
using OrleansDemo.Common.Infrastructure;

namespace Orleans.Infrastructure.Repository.User;

public interface ISysUserRepository: IRepository<SysUser,string>
{
}
