using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using HShopping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly dbHshoppingContext _context;
        public SearchController(dbHshoppingContext context)
        {
            _context = context;

        }
        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            List<Products> ls = new List<Products>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1) 
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            ls = _context.Products
                .AsNoTracking()
                .Include(a => a.Cat)
                .Where(x => x.ProductName.Contains(keyword))
                .OrderByDescending(x => x.ProductName)
                .Take(10)
                .ToList();
            if(ls ==null)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);
            }
        }
        public IActionResult FindCustomer(string keyword)
        {
            List<Customers> ls = new List<Customers>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListCustomersSearchPartial", null);
            }
            ls = _context.Customers
                .AsNoTracking()
                .Where(x => x.FullName.Contains(keyword))
                .OrderByDescending(x => x.FullName)
                .Take(10)
                .ToList();
            if (ls == null)
            {
                return PartialView("ListCustomersSearchPartial", null);
            }
            else
            {
                return PartialView("ListCustomersSearchPartial", ls);
            }
        }
        public IActionResult FindOrders(string  keyword)
        {
            List<Orders> ls = new List<Orders>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListOrdersSearchPartial", null);
            }
            ls = _context.Orders
                .AsNoTracking()
                .Where(x=>x.Note.Contains(keyword))
                .Take(10)
                .ToList();
            if (ls == null)
            {
                return PartialView("ListOrdersSearchPartial", null);
            }
            else
            {
                return PartialView("ListOrdersSearchPartial", ls);
            }
        }
    }
}
