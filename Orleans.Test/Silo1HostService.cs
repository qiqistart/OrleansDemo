using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Net;


namespace Orleans.Silo
{
    public class Silo1HostService : IHostedService
    {
        public ILogger<SiloHostService> logger;
        public ISiloHost Silo { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public Silo1HostService(ILogger<SiloHostService> logger)
        {
            this.logger = logger;
            Silo = new SiloHostBuilder()
               .Configure<ClusterOptions>(option =>
               {
                   option.ClusterId = "Orleans";
                   option.ServiceId = "Orleans";

               })
               .Configure<ClusterMembershipOptions>(option =>
                option.UseLivenessGossip = true
                )
               .UseAdoNetClustering(opt =>
               {
                   opt.Invariant = "MySql.Data.MySqlClient";
                   opt.ConnectionString = "Data Source=192.168.0.210;Database=orleans;AllowLoadLocalInfile=true;User ID=root;Password=root;allowPublicKeyRetrieval=true;pooling=true;CharSet=utf8;port=3306;sslmode=none;";
               })
               .ConfigureEndpoints(IPAddress.Parse("192.168.0.210"), 3020, 3021)
                 .UseDashboard(options => {
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
                Console.WriteLine("开始集群");
                await Silo.StartAsync(cancellationToken);
                Console.WriteLine("集群成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("集群失败"); ;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {

                Console.WriteLine("停止中");
                await Silo.StopAsync(cancellationToken);
                Console.WriteLine("停止完成");
            }
            catch (Exception)
            {

                Console.WriteLine("停止失败");
            }
        }
    }
}
