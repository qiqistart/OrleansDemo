using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Infrastructure.Repository.User;

namespace Orleans.Infrastructure;

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
        var  connectionString = configuration.GetValue<string>("ConnectionString:DbConnectionString");
        service.AddDbContext<OrleansDbContext>(opt =>
            {
                opt.UseMySQL(connectionString);
            }
        );
        service.AddScoped<ISysUserRepository, SysUserRepository>();
        return service;
    }
}