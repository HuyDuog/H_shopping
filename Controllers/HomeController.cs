using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HShopping.Models;
using HShopping.ModelView;
using Microsoft.EntityFrameworkCore;

namespace HShopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbHshoppingContext _context;
        public HomeController(ILogger<HomeController> logger , dbHshoppingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM();
            var lsproduct = _context.Products.AsNoTracking()
                .Where(x => x.Active == true)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
            List<ProductsHomeVM> lsproview = new List<ProductsHomeVM>();
            var lscat = _context.Categories.AsNoTracking()
                .OrderByDescending(x => x.CatId)
                .ToList();
        foreach(var item in lscat)
            {
                ProductsHomeVM product = new ProductsHomeVM();
                product.Categories = item;
                product.Products = lsproduct.Where(x => x.CatId == item.CatId).ToList();
                lsproview.Add(product);

            }
            model.Products = lsproview;
            ViewBag.AllProducts = lsproduct;
            return View(model);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
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
