using DrawingsWebApp.Data;
using DrawingsWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrawingsWebApp.Controllers
{
    public class DrawingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DrawingController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Drawing> objDrawingList = _db.Drawings.ToList();
            return View(objDrawingList);
        } 
    }
}
