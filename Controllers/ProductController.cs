using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment host;

        public ProductController(ApplicationDbContext _db, IHostingEnvironment _host)
        {
            db = _db;
            host = _host;
        }
        public IActionResult Index(int? page)
        {
            int pagesize = 10;
            int pageIndex;
            pageIndex = page == null ? 1 : (int)page;

            var lstProduct = db.Products.Include(x => x.Category).ToList();
            int pageCount = lstProduct.Count / pagesize + (lstProduct.Count % pagesize > 0 ? 1 : 0);
            ViewBag.PageSum = pageCount;
            ViewBag.PageIndex = pageIndex;
            return View(lstProduct.Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.lstCategory = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product objProduct, IFormFile file)
        {
            if (ModelState.IsValid) //kiem tra hop le du lieu
            {
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(host.WebRootPath, @"images/products");
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    objProduct.ImageUrl = @"images/products/" + filename;
                }
                db.Products.Add(objProduct);
                db.SaveChanges();
                TempData["success"] = "Product inserted success";
                return RedirectToAction("Index");
            }
            ViewBag.lstCategory = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View();
        }
        public IActionResult Edit(int id)
        {
            var objProduct = db.Products.Find(id);
            if (objProduct == null)
                return NotFound();
            ViewBag.lstCategory = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(objProduct);
        }
        [HttpPost]
        public IActionResult Edit(Product objProduct, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(host.WebRootPath, @"images/products");
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    if (!String.IsNullOrEmpty(objProduct.ImageUrl))
                    {
                        var oldFilePath = Path.Combine(host.WebRootPath, objProduct.ImageUrl);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    objProduct.ImageUrl = @"images/products/" + filename;
                }
                db.Products.Update(objProduct);
                db.SaveChanges();
                TempData["success"] = "Product updated success";
                return RedirectToAction("Index");
            }
            ViewBag.lstCategory = db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(objProduct);
        }
        public IActionResult Delete(int id)
        {
            var objProduct = db.Products.Find(id);
            if (objProduct == null)
                return NotFound();
            if (!String.IsNullOrEmpty(objProduct.ImageUrl))
            {
                var oldFilePath = Path.Combine(host.WebRootPath, objProduct.ImageUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            db.Products.Remove(objProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}


