
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Silo.Configuration;
using Orleans.Silo.HostService;




var host = new HostBuilder()
.ConfigureHostConfiguration(config =>
{
   config.AddJsonFile("ConfigJson/siloconfig.json");
   AppSetting._cfg = config.Build();
})
.ConfigureServices((hostbuilderContext, services) =>
{
  services.AddSingleton<SiloHostService>();
  services.AddSingleton<IHostedService>(_ => _.GetService<SiloHostService>());
  services.AddSingleton(_ => _.GetService<SiloHostService>().Silo);
}).Build();
await host.RunAsync();

