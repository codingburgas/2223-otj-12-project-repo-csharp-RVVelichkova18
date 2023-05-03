using ForestrySystem.Models;
using ForestrySystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ForestrySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ImagesService _imgService;

        public HomeController(ILogger<HomeController> logger,
        ImagesService imgService)
        {
            _logger = logger;
            this._imgService = imgService;
        }

        public IActionResult Index()
        {
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