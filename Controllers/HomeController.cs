using DrawingsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DrawingsWebApp.Controllers
{
    public class HomeController : Controller
    {

        string userMode = "engineer";


        public Boolean isEngineer()
        {
            if (userMode == "engineer")
                return true;

            return false;
        }



        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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



        [HttpPost]
        public IActionResult Index(string userSelect)
        {
            userMode = userSelect;
            
            return View();

        }
}
