using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Silo.Configuration.ConfigModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Orleans.Silo.Configuration;

/// <summary>
/// 静态
/// </summary>
public static class AppSetting
{
    /// <summary>
    /// 
    /// </summary>
    public static IConfiguration _cfg { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public static ClusterConfig ClusterConfig => _cfg.GetSection(nameof(ClusterConfig)).Get<ClusterConfig>();


    /// <summary>
    /// 
    /// </summary>
    public static IPAddress IPAddress=> _cfg.GetSection(nameof(IPAddress)).Get<IPAddress>();


    /// <summary>
    /// 
    /// </summary>
    public static Orleans.Silo.Configuration.ConfigModel.Clustering Clustering => _cfg.GetSection(nameof(Clustering)).Get<Orleans.Silo.Configuration.ConfigModel.Clustering>();


    /// <summary>
    /// 
    /// </summary>
    public static GrainStorage GrainStorage => _cfg.GetSection(nameof(GrainStorage)).Get<GrainStorage>();
}

