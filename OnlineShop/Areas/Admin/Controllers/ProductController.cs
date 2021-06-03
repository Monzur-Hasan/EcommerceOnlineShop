using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data;
using OnlineShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbContext;
        private IHostingEnvironment _hostingEnvironment;  //go to directory root folder
        public ProductController(ApplicationDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }
        //Index
        public IActionResult Index()
        {
            return View(_dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList());
        }

        [HttpPost]
        public IActionResult Index(decimal? lowPrice, decimal? highPrice)
        {
            var products = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).Where(c => c.Price >= lowPrice && c.Price <= highPrice).ToList();
            
            if(lowPrice == null || highPrice == null)
            {
                products = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).ToList();
            }

            return View(products);
        }
        //Create
        public ActionResult Create()
        {
            ViewData["productTypeId"] = _dbContext.ProductTypes.ToList();
            ViewData["tagId"] = _dbContext.TagNames.ToList();
            //ViewData["TagId"] = new SelectList(_dbContext.TagNames, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {

            if (ModelState.IsValid)
            {
                var isExist = _dbContext.Products.FirstOrDefault(c => c.Name == products.Name);
                if(isExist != null)
                {
                    ViewBag.message = "Product name is already exists!";

                    ViewData["productTypeId"] = _dbContext.ProductTypes.ToList();
                    ViewData["tagId"] = _dbContext.TagNames.ToList();

                    return View(products);
                }

                if (image != null)
                {
                    var name = Path.Combine(_hostingEnvironment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "Images/noimage.PNG";
                }

                _dbContext.Products.Add(products);
                await _dbContext.SaveChangesAsync();
                TempData["Save"] = "Product has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        //Edit
        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = _dbContext.ProductTypes.ToList();
            ViewData["tagId"] = _dbContext.TagNames.ToList();

            if (id == null)
            {
                return NotFound();
            }

            var product = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var isExist = _dbContext.Products.FirstOrDefault(c => c.Name == products.Name);
                if (isExist != null)
                {
                    ViewBag.message = "Product name is already exists!";

                    ViewData["productTypeId"] = _dbContext.ProductTypes.ToList();
                    ViewData["tagId"] = _dbContext.TagNames.ToList();

                    return View(products);
                }

                if (image != null)
                {
                    var name = Path.Combine(_hostingEnvironment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    products.Image = "Images/noimage.PNG";
                }

                _dbContext.Products.Update(products);
                await _dbContext.SaveChangesAsync();
                TempData["Edit"] = "Product has been updated";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        //Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag)
                          .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTag)
                          .FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
                       
            var product = _dbContext.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                TempData["Delete"] = "Product has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
