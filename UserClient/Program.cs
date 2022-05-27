var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//identity server
builder.Services.AddAuthentication(option => {
    option.DefaultScheme = "Cookies";
    option.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies")
 .AddOpenIdConnect("oidc", option =>
 {
     option.Authority = "https://localhost:7151/";
     option.ClientId = "faezehjafarpour";
     option.GetClaimsFromUserInfoEndpoint = true;
     option.SignInScheme = "Cookies";
     option.Scope.Add("profile");
 });

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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
