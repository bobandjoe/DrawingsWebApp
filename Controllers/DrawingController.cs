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

        //GET
        public IActionResult Upload()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(DrawingVm model)
        {
            Drawing drawing = new Drawing
            {
                FileName = model.FileName
            };
            
            //Convert the image into bytes array type
            using (MemoryStream ms = new MemoryStream())
            {
                await model.Image.CopyToAsync(ms);
                drawing.Image = ms.ToArray();
            }
                
            //Add to database
            _db.Drawings.Add(drawing);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
