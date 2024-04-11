using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Silo.Configuration;
using System.Net;


namespace Orleans.Silo.HostService;

public class SiloHostService : IHostedService
{
    public ILogger<SiloHostService> logger;
    public ISiloHost Silo { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="cfg"></param>
    public SiloHostService(ILogger<SiloHostService> logger)
    {
        this.logger = logger;
        Silo = new SiloHostBuilder()
           .Configure<ClusterOptions>(option =>
           {

               option.ClusterId = AppSetting.ClusterConfig.ClusterId;
               option.ServiceId = AppSetting.ClusterConfig.ServiceId;

           })
           .Configure<ClusterMembershipOptions>(option =>
            option.UseLivenessGossip = true
            )
           .UseAdoNetClustering(opt =>
            {
                opt.Invariant = AppSetting.Clustering.Invariant;
                opt.ConnectionString = AppSetting.Clustering.ConnectionString;
            })
           .ConfigureEndpoints(IPAddress.Parse(AppSetting.IPAddress.ipString), AppSetting.IPAddress.siloPort, AppSetting.IPAddress.gatewayPort)
             //  .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IHalloGrains).Assembly).WithReferences())
             .UseDashboard(options =>
             {
                 options.Username = "admin";
                 options.Password = "12345";
                 options.Host = "*";
                 options.Port = 8080;
                 options.HostSelf = true;
                 options.CounterUpdateIntervalMs = 1000;
             })
           .Build();

    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Console.Out.WriteLineAsync("开始集群");
            await Silo.StartAsync(cancellationToken);
            await Console.Out.WriteLineAsync("集群成功");
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            await Console.Out.WriteLineAsync("集群失败");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {

            await Console.Out.WriteLineAsync("停止中");
            await Silo.StopAsync(cancellationToken);
            await Console.Out.WriteLineAsync("停止完成");
        }
        catch (Exception)
        {

            await Console.Out.WriteLineAsync("停止失败");
        }
    }
}

