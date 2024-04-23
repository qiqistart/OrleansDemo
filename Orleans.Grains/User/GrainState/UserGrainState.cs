using Orleans.Domain.Entity.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Grains.User.GrainState;

[Serializable]
public class UserGrainState
{
    /// <summary>
    /// 
    /// </summary>

    public SysUser SysUser { get; set; }
}
