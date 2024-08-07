using Crud_Operations_in_Asp.Net_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Crud_Operations_in_Asp.Net_Core.Controllers
{

    [Authorize(Roles = "Admin")]
	public class ProductController : Controller
    {
        private readonly AssignmentDbContext context;

        public ProductController(AssignmentDbContext _context)
        {
            this.context = _context;
        }

		// Read Products From Database:

		public IActionResult Index()
        {
            var prdList = context.TblProducts.ToList();

            return View(prdList);
        }

        // Create Products in Database:

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblProduct products, IFormFile file)
        {
            string imageName = DateTime.Now.ToString("yymmddhhmmss");
            imageName += Path.GetFileName(file.FileName);
            var imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/uploads");
            var imageValue = Path.Combine(imagePath, imageName);

            using (var stream = new FileStream(imageValue, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var dbimage = Path.Combine("/uploads", imageName);
            products.Pimage = dbimage;
            context.TblProducts.Add(products);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Edit Products in Database:

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var p_id = context.TblProducts.Find(Id);
            if(p_id == null)
            {
                return View();
            }
            else
            {
                return View(p_id);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TblProduct p, IFormFile file, string oldImage)
        {
            var dbImage = "";

            if (file != null && file.Length > 0)
            {
                string imageName = DateTime.Now.ToString("yymmddhhmmss");
                imageName += Path.GetFileName(file.FileName);
                var imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/uploads");
                var imageValue = Path.Combine(imagePath, imageName);

                using (var stream = new FileStream(imageValue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var dbimage = Path.Combine("/uploads", imageName);
                p.Pimage = dbimage;
                context.TblProducts.Update(p);
                context.SaveChanges();
            }
            else
            {
                p.Pimage = oldImage;
                context.TblProducts.Update(p);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var del_Id = context.TblProducts.FirstOrDefault(x => x.Pid == id);
            if (del_Id != null)
            {
                return View(del_Id);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TblProduct x)
        {
            context.TblProducts.Remove(x);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
