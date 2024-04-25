using Microsoft.Extensions.DependencyInjection;
using NewLife.Data;

namespace OrleansDemo.Common.SnowflakeModule;
public static class SnowflakeRegistet
{

    /// <summary>
    /// 注册Snowflake ID生成器
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    public static IServiceCollection AddSnowflake(this IServiceCollection service)
    {
        service.AddSingleton(opt=> new Snowflake());
        return service;
    }
}
