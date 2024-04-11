using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Orleans.Application;

public static class ApplicationRegister
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection addApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}
