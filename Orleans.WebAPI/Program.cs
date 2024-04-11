using Orleans.Infrastructure;
using OrleansDemo.Common;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ClusterClientHostedService>();
builder.Services.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>());
builder.Services.AddSingleton(_ => _.GetService<ClusterClientHostedService>().clusterClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();