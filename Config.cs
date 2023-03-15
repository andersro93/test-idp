using Duende.IdentityServer.Models;

namespace TestIdp;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new ("ssn", "Social Security Number", new []{"ssn"}),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[] { };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "511536EF-F270-4058-80CA-1C89C192F69A",
                ClientName = "Maskin client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "openid", "profile" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
                ClientName = "Frontend",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    
                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:3000/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:3000/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:3000/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile" }
            },
        };
}
