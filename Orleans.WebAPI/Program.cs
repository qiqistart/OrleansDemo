using Orleans.WebAPI.Identity;
using Orleans.WebAPI.IdentityServerConfig;
using OrleansDemo.Common.ClusterClient;
using Orleans.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentityServer()
   .AddDeveloperSigningCredential()
   //.AddInMemoryPersistedGrants()
       .AddInMemoryApiResources(Config.GetApis())
     .AddInMemoryClients(Config.GetClients())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddExtensionGrantValidator<SystemUserGrantValidator>();
builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication(options =>
{
    options.Authority =builder.Configuration.GetValue<string>("IdentityConfig:Authority");
    options.RequireHttpsMetadata = false;
    options.ApiSecret =builder.Configuration.GetValue<string>("IdentityConfig:ApiSecret");
});

builder.Services.AddClusterClient();
builder.Services.AddApplication();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();