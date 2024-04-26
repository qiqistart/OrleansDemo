using Orleans.Domain.Entity.UserAggregate;
using Orleans.Grains.User.GrainState;
using Orleans.Infrastructure.Repository.User;
using Orleans.Providers;

namespace Orleans.Grains.User;

[StorageProvider(ProviderName = "OrleansStorage")]
public class UserGrains : Grain<UserGrainState>, IUserGrains
{
    private readonly ISysUserRepository sysUserRepository;

    public UserGrains(ISysUserRepository sysUserRepository)
    {
        this.sysUserRepository = sysUserRepository;
    }
    /// <summary>
    ///    通过账号获取用户信息
    /// </summary>
    /// <param name="Account"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserByAccount(string Account)
    {
        var userData = await sysUserRepository.GetUserByAccount(Account);
        return userData;
    }

    /// <summary>
    ///    获取当前用户信息
    /// </summary>
    /// <returns></returns>
    public async Task<SysUser> GetUser()
    {
        return await Task.FromResult(this.State.SysUser);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    public async Task<SysUser> GetUserByUserId(string UserId)
    {
        var userData = GetUser().Result;
        if (userData == null)
        {
            userData = await sysUserRepository.GetUserByUserId(UserId);
            this.State.SysUser = userData;
            await this.WriteStateAsync();
        }
        return userData;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<bool> AddUser(SysUser user)
    {
        var addData = new SysUser(user.UserName, user.PassWord,user.Account,user.Avatar);
        await sysUserRepository.AddUser(addData);
        return true;
    }
}

