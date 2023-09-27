using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext _db)
        {
            _logger = logger;
            db = _db;
        }

        public IActionResult Index(int ?page)
        {
            int pageSize = 9;
            int pageIndex;
            pageIndex = page == null ? 1 : (int)page;

            var lstProduct = db.Products.Include(x => x.Category).ToList();
            int pageCount = lstProduct.Count / pageSize + (lstProduct.Count % pageSize > 0 ? 1 : 0);
            ViewBag.PageSum = pageCount;
            ViewBag.PageIndex = pageIndex;
            return View(lstProduct.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }
        public IActionResult Detail(int id)
        {
            var objProduct = db.Products.Find(id);
                if(objProduct == null)
            {
                return NotFound();
            }
            ViewBag.LIST_PRODUCT = db.Products.Where(x => x.CategoryId == objProduct.CategoryId && x.Id != id).Take(5).ToList();
            return View(objProduct);
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
    }
}
