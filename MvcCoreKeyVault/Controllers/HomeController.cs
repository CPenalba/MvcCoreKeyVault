using System.Diagnostics;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using MvcCoreKeyVault.Models;

namespace MvcCoreKeyVault.Controllers
{
    public class HomeController : Controller
    {

        //NECESITAMOS INYECTAR SECRETCLIENT
        private SecretClient secretClient;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, SecretClient client)
        {
            _logger = logger;
            this.secretClient = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string secretname)
        {
            KeyVaultSecret secret = await this.secretClient.GetSecretAsync(secretname);
            ViewData["SECRETO"] = secret.Value;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
