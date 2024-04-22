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
    /// 
    /// </summary>
    /// <param name="service"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrantructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<OrleansDbContext>(opt =>
            {
                opt.UseMySQL(configuration.GetConnectionString("DbConnectionString"));
            }
        );
        service.AddScoped<ISysUserRepository, SysUserRepository>();
        return service;
    }
}