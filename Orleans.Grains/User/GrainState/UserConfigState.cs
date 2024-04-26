using Orleans.Domain.Entity.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Grains.User.GrainState;
[Serializable]
public class UserConfigState
{
    /// <summary>
    /// 系统用户配置
    /// </summary>
    public SysUserConfig  sysUserConfig { get; set; }
}
