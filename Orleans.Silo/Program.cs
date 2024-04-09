
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Silo;

await new HostBuilder()
   .ConfigureServices(services =>
   {
       services.AddSingleton<SiloHostService>();
       services.AddSingleton<IHostedService>(_ => _.GetService<SiloHostService>());
       services.AddSingleton(_ => _.GetService<SiloHostService>().Silo);
   }).RunConsoleAsync();