using IdentityModel;
using IdentityServer4.Models;

namespace Orleans.WebAPI.IdentityServerConfig;
public static class Config
{

    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
            {
                new Client
                {
                    ClientId ="Admin",
                    AllowedGrantTypes = new List<string>()
                    {
                        "SystemUser",
                    },
                    ClientSecrets = {new Secret("secret".Sha256()) },
                    AllowedScopes=
                    {
                        "AdminAPI"
                    },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 3600 * 24 * 30,
                    SlidingRefreshTokenLifetime = 3600 * 24 * 60,
                   
                },
            };

    }

    public static IEnumerable<ApiResource> GetApis()
    {
        return new List<ApiResource>
            {
                new ApiResource("AdminAPI", "AdminAPI", new [] { JwtClaimTypes.Subject,"UserId"})
                {
                    Scopes = { "AdminAPI" }
                },
              
            };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new IdentityResource[]
        {
                new IdentityResources.OpenId()
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new ApiScope[]
        {
                new ApiScope("AdminAPI"),
        };
    }
}
