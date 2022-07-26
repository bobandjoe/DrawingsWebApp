using DrawingsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DrawingsWebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.UserModes = "engineer";
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


        [HttpPost]
        public IActionResult Index(FormModel myForm)
        {
            var selectedValue = myForm.currentUserMode;
            ViewBag.UserModes = selectedValue.ToString();
            return View();
        }


        
    }
}
