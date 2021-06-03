using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ProductTypesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //var data = _dbContext.ProductTypes.ToList();
            return View(_dbContext.ProductTypes.ToList());
        }

        //Create Get Action Method
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //Create Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken] //check valid token
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ProductTypes.Add(productTypes);
                await _dbContext.SaveChangesAsync();
                TempData["Save"] = "Product type has been saved";
                return RedirectToAction(actionName: nameof(Index));
            }

            return View(productTypes);
        }

        //Edit Get Action Method
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var productType = _dbContext.ProductTypes.Find(id);
            if(productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //Edit Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken] //check valid token
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Update(productTypes);
                await _dbContext.SaveChangesAsync();
                TempData["Edit"] = "Product type has been updated";
                return RedirectToAction(actionName: nameof(Index));  //show all data in index view
            }

            return View(productTypes);
        }

        //Details Get Action Method
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var productType = _dbContext.ProductTypes.Find(id);
            if(productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //---Details Post Action Method-----
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Details(ProductTypes productTypes)
        //{
        //    return RedirectToAction(nameof(Index));          
        //}


        //Delete Get Action Method
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var productType = _dbContext.ProductTypes.Find(id);
            if(productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Delete Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {
            if(id == null)
            {
                return NotFound();
            }
            if(id != productTypes.Id)
            {
                return NotFound();
            }

            var productType = _dbContext.ProductTypes.Find(id);
            if(productType == null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
               _dbContext.ProductTypes.Remove(productType);
                await _dbContext.SaveChangesAsync();
                TempData["Delete"] = "Product type has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
    }
}
