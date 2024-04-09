
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Silo;

await new HostBuilder()
   .ConfigureServices(services =>
   {
       services.AddSingleton<Silo1HostService>();
       services.AddSingleton<IHostedService>(_ => _.GetService<Silo1HostService>());
       services.AddSingleton(_ => _.GetService<Silo1HostService>().Silo);
   }).RunConsoleAsync();