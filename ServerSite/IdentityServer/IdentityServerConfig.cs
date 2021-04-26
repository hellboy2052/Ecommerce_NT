using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ServerSite.IdentityServer
{
    public static class IdentityServerConfig
    {
        private static IConfiguration _configuration;

        public static IConfiguration Configuration
        {
            get
            {
                return _configuration;
            }
        }

        public static void InitConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
             new ApiScope[]
             {
                  new ApiScope("rookieshop.api", "Rookie Shop API")
             };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "rookieshop.api" }
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { _configuration["CustomerSiteUri:Default"] +"/signin-oidc" },

                    PostLogoutRedirectUris = { _configuration["CustomerSiteUri:Default"] +"/signout-callback-oidc" },


                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookieshop.api"
                    }
                },
                new Client
                {
                    ClientId = "swagger",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris =           { _configuration["BackendUrl:Default"] +"/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { _configuration["BackendUrl:Default"] +"/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins =     {_configuration["BackendUrl:Default"]  },


                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookieshop.api"
                    }
                },
                 new Client
                {
                    ClientId = "react-admin",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:3000/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:3000/signout-oidc" },
                    AllowedCorsOrigins={"http://localhost:3000"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rookieshop.api"
                    },
                    AllowAccessTokensViaBrowser=true,
                    RequireConsent=false,
                },
            };
    }
}