using DrawingsWebApp.Data;
using DrawingsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

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

        public IActionResult RenderImage(int id)
        {
            Drawing drawing = _db.Drawings.Find(id);
            byte[] byteData = drawing.Image;
            return File(byteData, "image/png");
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
            if (ModelState.IsValid)
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
                TempData["success"] = "Drawing uploaded successfully";

                return RedirectToAction("Index");
            }
            return View(model);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id.Value == 0)
            {
                return NotFound();
            }
            var drawingFromDb = _db.Drawings.Find(id);

            if (drawingFromDb == null)
            {
                return NotFound();
            }

            return View(drawingFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Drawing model)
        {
            _db.Drawings.Attach(model);
            _db.Entry(model).Property(x => x.FileName).IsModified = true;
            _db.SaveChanges();
            TempData["success"] = "Drawing name updated successfully";

            return RedirectToAction("Index");

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return NotFound();
            }
            var drawingFromDb = _db.Drawings.Find(id);

            if (drawingFromDb == null)
            {
                return NotFound();
            }

            return View(drawingFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var model = _db.Drawings.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _db.Drawings.Remove(model);
            _db.SaveChanges();
            TempData["success"] = "Drawing deleted successfully";

            return RedirectToAction("Index");

        }

    }
}
