using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Grains.User;
using Orleans.Hosting;
using Orleans.Infrastructure;
using Orleans.Infrastructure.Repository.User;
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
           .AddAdoNetGrainStorage(AppSetting.GrainStorage.Name, options =>
           {
               options.Invariant = AppSetting.GrainStorage.Invariant;
               options.ConnectionString = AppSetting.GrainStorage.ConnectionString;
               options.UseJsonFormat = AppSetting.GrainStorage.UseJsonFormat;

           })
           .UseTransactions()
           
           .UseAdoNetClustering(opt =>
            {
                opt.Invariant = AppSetting.GrainStorage.Invariant;
                opt.ConnectionString = AppSetting.GrainStorage.ConnectionString;
            })
      
           .ConfigureEndpoints(IPAddress.Parse(AppSetting.IPAddress.ipString), AppSetting.IPAddress.siloPort, AppSetting.IPAddress.gatewayPort)
              .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IUserGrains).Assembly).WithReferences())
              .ConfigureServices(services =>
              {
                  services.AddInfrantructure(AppSetting._cfg);

              })
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

