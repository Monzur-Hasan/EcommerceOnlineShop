using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using System.Diagnostics;
using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using OnlineShop.Utility;

namespace OnlineShop.Controllers
{

    [Area("Customer")]
    public class HomeController : Controller 
    {
        private ApplicationDbContext _dbContext;
        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList());
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

        //Details product
        public ActionResult Detail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product = _dbContext.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //Add to Cart post Action
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)  
        {
            List<Products> products = new List<Products>();

            if(id == null)
            {
                return NotFound();
            }

            var product = _dbContext.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if(product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Products>>("products"); // 2. for catch session List<> data 
            if (products == null)
            {
                products = new List<Products>();
            }

            products.Add(product); //1. for catch session single data 

            HttpContext.Session.Set("products", products); //update session data 

            return RedirectToAction(nameof(Index));
        }

        // 2=> RemoveCart Get Action for remove Data in Cart View 
        [ActionName("Remove")]
        public IActionResult RemoveCart(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products"); //Retrieve session data
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products); //Update session data
                }
            }
            return RedirectToAction(nameof(Index));
        }
        // 1 => Remove to Cart post Action
        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products"); //Retrieve session data
            if(products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if(product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products); //Update session data
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //Get Cart Action
        public IActionResult Cart()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products == null)
            {
                products = new List<Products>();
            }
            return View(products);
        }
    }
}
