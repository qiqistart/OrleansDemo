using NewLife.Data;
using OrleansDemo.Common.ServiceProviderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Common.SnowflakeModule;

public static class SnowflakeHelper
{
    /// <summary>
    /// 生成一个新的ID
    /// </summary>
    /// <returns></returns>
    public static string NewId()
    {
        var serviceProvider =new Snowflake();
        return Convert.ToString(serviceProvider.NewId()); 
    }
}
