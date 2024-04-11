
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Silo.Configuration;
using Orleans.Silo.HostService;
using Orleans.Infrastructure;
await new HostBuilder()
    .ConfigureHostConfiguration(config =>
    {
        config.AddJsonFile("ConfigJson/siloconfig.json");
        AppSetting._cfg = config.Build();
    })
   .ConfigureServices(services =>
   {
       services.AddSingleton<SiloHostService>();
       services.AddSingleton<IHostedService>(_ => _.GetService<SiloHostService>());
       services.AddSingleton(_ => _.GetService<SiloHostService>().Silo);
       services.AddInfrantructure(AppSetting._cfg);
   }).RunConsoleAsync();