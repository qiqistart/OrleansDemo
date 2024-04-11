﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Net;


namespace Orleans.Silo
{
    public class SiloHostService : IHostedService
    {
        public ILogger<SiloHostService> logger;
        public ISiloHost Silo { get; }

        private readonly IConfiguration cfg;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="cfg"></param>
        public SiloHostService(ILogger<SiloHostService> logger, IConfiguration cfg)
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
                    opt.ConnectionString = "Data Source=192.168.0.105;Database=OrleansDemo;AllowLoadLocalInfile=true;User ID=root;Password=123456;allowPublicKeyRetrieval=true;pooling=true;CharSet=utf8;port=3306;sslmode=none;";
                })
               .ConfigureEndpoints(IPAddress.Parse("192.168.0.105"), 2020, 2021)
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
}
