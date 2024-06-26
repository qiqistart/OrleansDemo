﻿using Microsoft.EntityFrameworkCore;
using Orleans.Domain.Entity.UserAggregate;
using Orleans.Grains.User.GrainState;
using Orleans.Infrastructure;
using Orleans.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Grains.User;
/// <summary>
/// 
/// </summary>
[StorageProvider(ProviderName = "OrleansStorage")]
public class UserConfigGrains : Grain<UserConfigState>, IUserConfigGrains
{
    private readonly IGrainFactory grainFactory;

    /// <summary>
    /// 
    /// </summary>
    private OrleansDbContext _orleansDb;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orleansDb"></param>
    public UserConfigGrains(OrleansDbContext _orleansDb, IGrainFactory grainFactory)
    {
        this._orleansDb = _orleansDb;
        this.grainFactory = grainFactory;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<SysUserConfig> GetUserConfig(string UserId)
    {
        var userConfig = this.State.sysUserConfig;
        if (userConfig == null)
        {
            userConfig = await _orleansDb.SysUserConfig.FirstOrDefaultAsync(x => x.UserId == UserId);
            await WriteStateAsync();
        }
        return userConfig;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userConfig"></param>
    /// <returns></returns>
    public async Task<bool> AddUserConfig(SysUserConfig userConfig)
    {
        var addData =new SysUserConfig(userConfig.UserId, userConfig.AccountBalance, userConfig.IsOpenOverdraftConsumption, userConfig.OverdraftConsumptionBalance);
        await _orleansDb.SysUserConfig.AddAsync(addData);
        await _orleansDb.SaveChangesAsync();
        return true;    
    }
}
