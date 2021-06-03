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
    public class SpecialTagController : Controller
    {

        private ApplicationDbContext _dbContext;

        public SpecialTagController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //var data = _dbContext.ProductTypes.ToList();
            return View(_dbContext.TagNames.ToList());
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
        public async Task<IActionResult> Create(TagName tagName)
        {
            if (ModelState.IsValid)
            {
                _dbContext.TagNames.Add(tagName);
                await _dbContext.SaveChangesAsync();
                TempData["Save"] = "Tag name has been saved";
                return RedirectToAction(actionName: nameof(Index));
            }

            return View(tagName);
        }

        //Edit Get Action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagName = _dbContext.TagNames.Find(id);
            if (tagName == null)
            {
                return NotFound();
            }

            return View(tagName);
        }

        //Edit Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken] //check valid token
        public async Task<IActionResult> Edit(TagName tagName)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(tagName);
                await _dbContext.SaveChangesAsync();
                TempData["Edit"] = "Tag name has been updated";
                return RedirectToAction(actionName: nameof(Index));  //show all data in index view
            }

            return View(tagName);
        }

        //Details Get Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagName = _dbContext.TagNames.Find(id);
            if (tagName == null)
            {
                return NotFound();
            }

            return View(tagName);
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
            if (id == null)
            {
                return NotFound();
            }

            var tagName = _dbContext.TagNames.Find(id);
            if (tagName == null)
            {
                return NotFound();
            }
            return View(tagName);
        }

        //Delete Post Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, TagName tagName)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != tagName.Id)
            {
                return NotFound();
            }

            var tag = _dbContext.TagNames.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.TagNames.Remove(tag);
                await _dbContext.SaveChangesAsync();
                TempData["Delete"] = "Tag name has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(tagName);
        }
    }
}

