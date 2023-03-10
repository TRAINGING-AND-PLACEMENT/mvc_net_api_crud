using Microsoft.AspNetCore.Mvc;
using WithImageWeb.Data;
using WithImageWeb.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using WithImageWeb.Migrations;

namespace WithImageWeb.Controllers
{
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ImageController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            var images = _db.Images.ToList();
            return View(images);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ImageCreateModel model) 
        {
            if (ModelState.IsValid)
            {
                var path = _env.WebRootPath;
                var filepath = "Content/Image/" + model.FileName.FileName;
                var fullpath = Path.Combine(path, filepath);
                UploadImage(model.FileName, fullpath);
                var data = new ImageCreateModel()
                {
                    Name = model.Name,
                    FileName = filepath,
                };
                _db.Images.Add(data);
                _db.SaveChanges();
                TempData["Success"] = "Image Added";
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        public void UploadImage(IFormFile file,string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            file.CopyTo(fileStream);
        }
        public IActionResult Edit(int? id) 
        {
            /*if (id == null || id == 0)
            {
                return NotFound();
            }*/
            var images = _db.Images.Where(x => x.Id == id).SingleOrDefault();
            if(images != null)
            {
                return View(images);
            }
            else { return RedirectToAction("Index"); }
        }
        public IActionResult Delete(int id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = _db.Images.SingleOrDefault(x => x.Id == id);
                if (data != null)
                {
                    string deletefolder = Path.Combine(_env.WebRootPath, "Content/Image/");
                    string currectimage = Path.Combine(Directory.GetCurrentDirectory(), deletefolder, data.FileName);
                    if(currectimage != null)
                    {
                        if(System.IO.File.Exists(currectimage))
                        {
                            System.IO.File.Delete(currectimage);
                        }
                    }
                    _db.Images.Remove(data);
                    _db.SaveChanges();
                    TempData["success"] = "Image Deleted!";
                }
            }
            return RedirectToAction("Index");
        }
    }
}
