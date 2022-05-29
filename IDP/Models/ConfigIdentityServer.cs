using IdentityServer4.Models;
using System.Security.Claims;

namespace IDP.Models
{
    public static class ConfigIdentityServer
    {

        public static List<IdentityServer4.Test.TestUser> GetTestUsers()
        {
            return new List<IdentityServer4.Test.TestUser>{
                new IdentityServer4.Test.TestUser
                {
                    SubjectId = "1",
                    IsActive = true,
                    Password = "123456",
                    Username = "faezeh",
                    Claims = new List<Claim>
                          {
                              new Claim(ClaimTypes.Email ,"faezeh") ,
                              new Claim(ClaimTypes.MobilePhone ,"09034882131") ,
                              new Claim("name" , "faezeh jafarpour")
                          }
                }
            };

        }

        public static List<IdentityResource> GetIdentityServer()
        {
            return new List<IdentityResource>
            {
                    new IdentityResources.OpenId() ,
                    new IdentityResources.Address() ,
                    new IdentityResources.Email() ,
                    new IdentityResources.Phone() ,
                    new IdentityResources.Profile() ,
            };
        }

        public static List<Client> GetClient()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "faezehjafarpour",
                    ClientSecrets = new List<Secret> { new Secret("123456".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:7206/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7206/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Email,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Phone,
                    },
                    RequireConsent = true,
                } ,

                new Client
                {
                     ClientId = "ApiWeatherId",
                    ClientSecrets = new List<Secret> { new Secret("123456".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =new []{"ApiWeather"}
  
                }
            };
        }

        public static List<ApiResource> GetApiResource()
        {
            return new List<ApiResource>
            {
                new ApiResource("ApiWeather" , "وب سرویس هواشناسی")
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
        {
            new ApiScope("ApiWeather","سرویس هواشناسی")
            };

        }
    }
}
