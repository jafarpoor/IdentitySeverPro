using IdentityServer4.Models;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// identity server
builder.Services.AddIdentityServer()
                  .AddDeveloperSigningCredential()
                  .AddTestUsers(new List<IdentityServer4.Test.TestUser> {
                      new IdentityServer4.Test.TestUser
                      {
                          SubjectId="1" ,
                          IsActive =true ,
                          Password="123456",
                          Username="faezeh" ,
                          Claims = new List<Claim>
                          {
                              new Claim(ClaimTypes.Email ,"faezeh") ,
                              new Claim(ClaimTypes.MobilePhone ,"09034882131") ,
                              new Claim("name" , "faezeh jafarpour") 
                          }
                      }
                  })
                  .AddInMemoryIdentityResources(new List<IdentityResource>
                  {
                    new IdentityResources.OpenId() ,
                    new IdentityResources.Address() ,
                    new IdentityResources.Email() ,
                    new IdentityResources.Phone() ,
                    new IdentityResources.Profile() ,
                  })
                  .AddInMemoryApiResources(new List<ApiResource> { })
                  .AddInMemoryClients(new List<Client> {
                        new Client {
                        ClientId="faezehjafarpour",
                        ClientSecrets=new List<Secret> { new Secret("123456".Sha256()) },
                        AllowedGrantTypes=GrantTypes.Implicit,
                        RedirectUris={"https://localhost:7206/signin-oidc" },
                        PostLogoutRedirectUris={"https://localhost:7206/signout-callback-oidc"},
                        AllowedScopes =new List<string>
                        {
                             IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                             IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                             IdentityServer4.IdentityServerConstants.StandardScopes.Email,
                             IdentityServer4.IdentityServerConstants.StandardScopes.Phone,
                        },
                        RequireConsent=true,
                     }
                  });

//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
