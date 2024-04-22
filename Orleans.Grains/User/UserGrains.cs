using Orleans.Domain.Entity.UserAggregate;
using Orleans.Grains.User.GrainState;
using Orleans.Infrastructure.Repository.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Grains.User;

public class UserGrains : Grain<UserGrainState>, IUserGrains
{
    private readonly ISysUserRepository sysUserRepository;

    public UserGrains(ISysUserRepository sysUserRepository)
    {
        this.sysUserRepository = sysUserRepository;
    }
    /// <summary>
    ///     获取用户信息
    /// </summary>
    /// <param name="Account"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserByAccount(string Account)
    {
        var userData = await sysUserRepository.GetUserByAccount(Account);
        this.State.SysUser = userData;
        await WriteStateAsync();
        return userData;
    }
}

