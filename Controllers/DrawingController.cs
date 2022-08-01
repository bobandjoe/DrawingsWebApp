using DrawingsWebApp.Data;
using DrawingsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult DetailedView(int? id)
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

        //GET
        public IActionResult CreateComment(int? id)
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
        public IActionResult CreateComment(Drawing model, string commentString, string dateTimeString)
        {
            if (commentString == null)
            {
                model.Comment = model.FormComment;
                model.CommentDateTime = model.FormCommentDateTime.ToString();
            } 
            else
            {
                string[] comments = commentString.Split("@#$%&");
                string[] dateTimes = dateTimeString.Split("@#$%&");
                string[] updatedComments = new string[comments.Length + 1];
                string[] updatedDateTimes = new string[dateTimes.Length + 1];
                for (int i = 0; i < comments.Length; i++)
                {
                    updatedComments[i] = comments[i];
                    updatedDateTimes[i] = dateTimes[i];
                }
                updatedComments[updatedComments.Length - 1] = model.FormComment;
                updatedDateTimes[updatedDateTimes.Length - 1] = model.FormCommentDateTime.ToString();
                model.Comment = String.Join("@#$%&", updatedComments);
                model.CommentDateTime = String.Join("@#$%&", updatedDateTimes);
            }
            _db.Drawings.Attach(model);
            _db.Entry(model).Property(x => x.Comment).IsModified = true;
            _db.Entry(model).Property(x => x.CommentDateTime).IsModified = true;
            _db.SaveChanges();
            TempData["success"] = "Comment added successfully";

            return RedirectToAction("DetailedView", new { id = model.Id});
        }

        //GET
        public IActionResult Upload()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(Drawing model)
        {

            //Convert the image into bytes array type
            using (MemoryStream ms = new MemoryStream())
            {
                await model.FormImage.CopyToAsync(ms);
                model.Image = ms.ToArray();
            }

            //Add to database
            _db.Drawings.Add(model);
            await _db.SaveChangesAsync();
            TempData["success"] = "Drawing uploaded successfully";

            return RedirectToAction("Index");

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
