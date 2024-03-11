using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StackyWeb.Data;
using StackyWeb.Models;

namespace StackyWeb.Controllers
{
    public class ShoeController : Controller
    {
        private readonly ApplicationDbContext ct;
        private readonly IWebHostEnvironment wbhost;
        public ShoeController(ApplicationDbContext _ct, IWebHostEnvironment env)
        {
            ct = _ct;
            wbhost = env;
        }
        public IActionResult Index()
        {
            List<Models.Shoe> x = ct.Shoes.ToList();
            return View(x);
        }

        [HttpGet]
        public IActionResult Upsert(Guid? id)
        {
            if (id != null)
            {
                //Devo fare update
                Shoe ShoeFromDb = ct.Shoes.Find(id);
                if(ShoeFromDb == null)
                {
                    return NotFound();
                }
                return View(ShoeFromDb);
            }
            else
            {
                //Devo fare create
                Shoe newShoe = new Shoe();
                return View(newShoe);
            }

        }

        [HttpPost]
        public IActionResult Upsert(Shoe x, IFormFile? file)
        {
            //se sto creando
            string wwwRootPath = wbhost.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(x.UrlImg))
                {
                    //voglio eliminare vecchia foto e inserire la nuova
                    var oldImgPath = Path.Combine(wwwRootPath, x.UrlImg.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }



                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                x.UrlImg = @"\images\product\" + fileName;
            }

            if( x.Id == Guid.Empty)
            {
                //create
                ct.Shoes.Add(x);
            }
            else
            {
                //update
                ct.Shoes.Update(x);
            }

            
            ct.SaveChanges();

            TempData["message"] = "Product Created Succesfully";

            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
			string wwwRootPath = wbhost.WebRootPath;
            Shoe x = ct.Shoes.Find(id);

			if (x != null)
            {
				//are you sure button?

                //elimino da wwrot img
				if (!string.IsNullOrEmpty(x.UrlImg))
				{
					//voglio eliminare vecchia foto e inserire la nuova
					var ImgPath = Path.Combine(wwwRootPath, x.UrlImg.TrimStart('\\'));

					if (System.IO.File.Exists(ImgPath))
					{
						System.IO.File.Delete(ImgPath);
					}
				}

				ct.Shoes.Remove(x);
            }

            ct.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
