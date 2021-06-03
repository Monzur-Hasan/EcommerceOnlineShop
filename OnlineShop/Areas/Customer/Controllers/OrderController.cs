using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Data;
using OnlineShop.Models;
using Microsoft.AspNetCore.Http;
using OnlineShop.Utility;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get Checkout Action
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order anOrder)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if(products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    //anOrder.OrderDetails = new List<OrderDetails>();
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }

            anOrder.OrderNo = GetOrderNo();
            _dbContext.Orders.Add(anOrder);
            await _dbContext.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());

            return View();
        }

        //Get Order No Method
        public string GetOrderNo()
        {
            int rowCount = _dbContext.Orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}
