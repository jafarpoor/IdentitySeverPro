using IdentityServer4.Models;
using IDP.Models;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// identity server
builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(ConfigIdentityServer.GetTestUsers())
                .AddInMemoryIdentityResources(ConfigIdentityServer.GetIdentityServer())
                .AddInMemoryApiResources(ConfigIdentityServer.GetApiResource())
                .AddInMemoryClients(ConfigIdentityServer.GetClient())
                .AddInMemoryApiScopes(ConfigIdentityServer.GetApiScopes());

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
