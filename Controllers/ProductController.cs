using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HShopping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace HShopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly dbHshoppingContext _context;

        public ProductController(dbHshoppingContext context)
        {
            _context = context;
        }
        [Route("/Shop.html", Name = "ShopProducts")]
        public IActionResult Index(int? page)
        {
            try
            {
                var pagenumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var isCustomers = _context.Products.AsNoTracking().OrderByDescending(x => x.DateCreated);
                PagedList<Products> model = new PagedList<Products>(isCustomers, pagenumber, pageSize);
                ViewBag.CurrentPage = pagenumber;
                return View(model);

            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Route("/{catname}", Name = "ListProducts")]
        public IActionResult List( string  catname , int page = 1)
        {
            try
            {

                var pageSize = 10;
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x=>x.CatName == catname);
                var isCustomers = _context.Products.AsNoTracking().Where(x => x.CatId == danhmuc.CatId).OrderByDescending(x => x.DateCreated);
                PagedList<Products> model = new PagedList<Products>(isCustomers, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCate = danhmuc;
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [Route("/{Title}-{id}.html",Name ="ProductDetails")]
        public IActionResult Details( int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsproduct = _context.Products.AsNoTracking()
                    .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true)
                    .OrderByDescending(x=>x.DateCreated)
                    .Take(4)
                    .ToList();
                ViewBag.Sanpham = lsproduct;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");

            }

        }

    }
}
