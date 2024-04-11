using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using OrleansDemo.Common.CommconConfig;
using System.Net;


namespace OrleansDemo.Common;

/// <summary>
/// 客户端
/// </summary>
public class ClusterClientHostedService : IHostedService
{
    public IClusterClient clusterClient { get; }

    /// <summary>
    /// 
    /// </summary>
    private IConfiguration _cfg;
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<ClusterClientHostedService> _logger;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_logger"></param>
    /// <param name="_cfg"></param>
    public ClusterClientHostedService(ILogger<ClusterClientHostedService> _logger, IConfiguration _cfg)
    {

        this._logger = _logger;
        this._cfg = _cfg;
        var SiloIpConfig = _cfg.GetSection("SiloIpConfig").Get<List<SiloIpConfig>>();
        List<IPEndPoint> iPEndPoints = new List<IPEndPoint>();
        foreach (var item in SiloIpConfig)
        {
            iPEndPoints.Add(new IPEndPoint(System.Net.IPAddress.Parse(item.ipString), item.gatewayPort));
        }
        var ClusterConfig = _cfg.GetSection("ClusterConfig").Get<ClusterConfig>();
        clusterClient = new ClientBuilder()
           .UseStaticClustering(iPEndPoints.ToArray())
           .Configure<ClusterOptions>(options =>
           {
               options.ClusterId = ClusterConfig.ClusterId;
               options.ServiceId = ClusterConfig.ServiceId;
           })
           .Build();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var attempt = 0;
        var maxAttempts = 10;
        var delay = TimeSpan.FromSeconds(3);

        return clusterClient.Connect(async error =>
        {

            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            if (++attempt < maxAttempts)
            {
                Console.WriteLine("开始连接orlenas客户端,第" + attempt + "次,最大连接次数:" + maxAttempts + "次");
                _logger.LogWarning("开始连接orlenas客户端,第" + attempt + "次,最大连接次数:" + maxAttempts + "次");
                try
                {
                    await Task.Delay(delay, cancellationToken);
                }
                catch (OperationCanceledException)
                {

                    return false;
                }
                return true;
            }
            else
            {
                Console.WriteLine("连接orleans客户端失败,尝试重新连接客户端");
                _logger.LogWarning("连接orleans客户端失败,尝试重新连接客户端");
                return false;
            }


        });

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            await clusterClient.Close();
        }
        catch (Exception)
        {
            Console.WriteLine("连接orleans客户端失败");
            _logger.LogWarning("连接orleans客户端失败");
        }
    }
}
