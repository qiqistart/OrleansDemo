using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Infrastructure.Repository.User;
namespace Orleans.Infrastructure;
using OrleansDemo.Common.SnowflakeModule;

/// <summary>
/// 
/// </summary>
public static class InfrantructureRegister
{
    /// <summary>
    /// 注册基础设施层
    /// </summary>
    /// <param name="service"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrantructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddSnowflake();
        var  connectionString = configuration.GetValue<string>("ConnectionString:DbConnectionString");
        var serverVersion = new MySqlServerVersion(new Version(8, 0,36));
       service.AddDbContext<OrleansDbContext>(opt =>
            {
                opt.UseMySql(connectionString, serverVersion);
            }
        );
        service.AddScoped<ISysUserRepository, SysUserRepository>();
        return service;
    }
}