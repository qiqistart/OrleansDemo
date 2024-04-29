using Orleans.Domain.Entity.UserAggregate;
using Orleans.Grains.User.GrainState;
using Orleans.Infrastructure;
using Orleans.Infrastructure.Repository.User;
using Orleans.Providers;
using System.Transactions;

namespace Orleans.Grains.User;

[StorageProvider(ProviderName = "OrleansStorage")]
public class UserGrains : Grain<UserGrainState>, IUserGrains
{
    private readonly ISysUserRepository sysUserRepository;

    private readonly IGrainFactory grainFactory;

    /// <summary>
    /// 
    /// </summary>
    private OrleansDbContext _orleansDb;
    public UserGrains(ISysUserRepository sysUserRepository, IGrainFactory grainFactory, OrleansDbContext _orleansDb)
    {
        this.sysUserRepository = sysUserRepository;
        this.grainFactory = grainFactory;
        this._orleansDb = _orleansDb;
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
    public async Task<SysUser> AddUser(SysUser user)
    {
    
            try
            {
                var addData = new SysUser(user.UserName, user.PassWord, user.Account, user.Avatar);
                await _orleansDb.SysUser.AddAsync(addData);

                await grainFactory.GetGrain<IUserConfigGrains>(addData.Id).AddUserConfig(new Domain.Entity.UserAggregate.SysUserConfig(addData.Id, 0, false, 0));
                await _orleansDb.SaveChangesAsync();
          
                return addData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

    }
}

