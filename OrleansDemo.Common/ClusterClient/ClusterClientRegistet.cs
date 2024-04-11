using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Common.ClusterClient;
public static class ClusterClientRegistet
{
    public static IServiceCollection AddClusterClient(this IServiceCollection service)
    {
        service.AddSingleton<ClusterClientHostedService>();
        service.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>());
        service.AddSingleton(_ => _.GetService<ClusterClientHostedService>().clusterClient);
        return service;
    }
}

