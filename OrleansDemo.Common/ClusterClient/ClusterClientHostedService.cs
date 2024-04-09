using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using System.Net;

namespace OrleansDemo.Common;

/// <summary>
/// 客户端
/// </summary>
public class ClusterClientHostedService : IHostedService
{
    public IClusterClient clusterClient { get; }

    private readonly ILogger<ClusterClientHostedService> _logger;
    public ClusterClientHostedService(ILogger<ClusterClientHostedService> _logger)
    {

        this._logger = _logger;
        IPEndPoint[] iPEndPoints = new IPEndPoint[1];
        iPEndPoints[0] = new IPEndPoint(IPAddress.Parse("192.168.0.210"), 2020);

        // Console.WriteLine("11");
        // iPEndPoints[] = new IPEndPoint(IPAddress.Parse(""), 1000);
        clusterClient = new ClientBuilder()
           .UseStaticClustering(iPEndPoints)
           .Configure<ClusterOptions>(options =>
           {
               options.ClusterId = "Orleans";
               options.ServiceId = "Orleans";
           })
           //.ConfigureLogging(builder => builder.AddProvider(loggerProvider))
           .Build();

    }
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
