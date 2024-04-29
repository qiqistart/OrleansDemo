using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Common.ServiceProviderModule;
public static class InternalApp
{
    /// <summary>
    /// 根容器
    /// </summary>
    public static IServiceProvider RootServiceProvider { get; set; }
    /// <summary>
    /// 创建新的服务域
    /// </summary>
    public static void CreateNewScopeServiceProvider()
    {
        RootServiceProvider = RootServiceProvider.CreateAsyncScope().ServiceProvider;
    }
    /// <summary>
    /// 获取请求生命周期的服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>

    public static TService GetService<TService>()
           where TService : class =>
           GetService(typeof(TService)) as TService;



    /// <summary>
    /// 获取请求生命周期的服务
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object GetService(Type type) => RootServiceProvider.GetService(type);
  
  
}

