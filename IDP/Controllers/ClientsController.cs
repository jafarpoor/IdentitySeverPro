using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ConfigurationDbContext _configuration;
        public ClientsController(ConfigurationDbContext configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var clients = _configuration.Clients.ToList();
            return View(clients);
        }

        public IActionResult Add()
        {
            _configuration.Clients.Add(new IdentityServer4.EntityFramework.Entities.Client
            {
                ClientId = "ApiWeatherId",
                ClientSecrets = new List<IdentityServer4.EntityFramework.Entities.ClientSecret>()
                 {
                      new IdentityServer4.EntityFramework.Entities.ClientSecret
                      {
                           Value = "123456".Sha256()
                      }
                 },
                AllowedGrantTypes = new List<IdentityServer4.EntityFramework.Entities.ClientGrantType>()
                 {
                      new IdentityServer4.EntityFramework.Entities.ClientGrantType()
                      {
                           GrantType ="ClientCredentials"
                      }
                 },
                AllowedScopes = new List<IdentityServer4.EntityFramework.Entities.ClientScope>()
                 {
                     new IdentityServer4.EntityFramework.Entities.ClientScope()
                     {
                          Scope="ApiWeather"
                     }
                 }

            });
            _configuration.SaveChanges();

            return Ok();
        }
    }
}
