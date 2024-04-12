using Microsoft.Extensions.DependencyInjection;
using Orleans.WebAPI.Identity;
using Orleans.WebAPI.IdentityServerConfig;
using OrleansDemo.Common.ClusterClient;
using Ubiety.Dns.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityServer()
   .AddDeveloperSigningCredential()
     .AddInMemoryClients(Config.GetClients())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddExtensionGrantValidator<SystemUserGrantValidator>();
builder.Services.AddAuthentication().AddJwtBearer(options =>
{

    options.Authority = "https://localhost:7085";
    options.RequireHttpsMetadata = false;
    options.Audience = "AdminAPI";
});

//builder.Services.AddClusterClient();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();