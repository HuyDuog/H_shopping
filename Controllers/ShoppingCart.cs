using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using HShopping.Extensions;
using HShopping.Models;
using HShopping.ModelView;
using Microsoft.AspNetCore.Mvc;

namespace HShopping.Controllers
{
    public class ShoppingCart : Controller
    {
        private readonly dbHshoppingContext _context;
        public INotyfService _notyfService { get; }
        public ShoppingCart(dbHshoppingContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        public List<CartItem> Giohang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("Giohang");
                if(gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;

            }
        }
        [HttpPost]
        [Route("/cart/add")]
        public IActionResult AddtoCart( int productID , int? amount)
        {
            List<CartItem> gioHang = Giohang;
            try
            {
 
                CartItem item = Giohang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
  
                        item.amount = item.amount + amount.Value;
                        HttpContext.Session.Set<List<CartItem>>("Giohang", gioHang);
          
    

                }
                else
                {
                    Products hh = _context.Products.SingleOrDefault(p => p.ProductId == productID);
                    item = new CartItem
                    {
                        amount = amount.HasValue ? amount.Value : 1,
                        product = hh
                    };
                    gioHang.Add(item);

                }
                HttpContext.Session.Set<List<CartItem>>("Giohang", gioHang);
                _notyfService.Success("Thêm sản phẩm thành công");
                return Json(new { success = true });
            }
            catch
            {
                _notyfService.Success("Thêm vào giỏ hàng thất bại");
                return Json(new { success = false });
            }

        }
        [HttpPost]
        [Route("/cart/update")]
        public IActionResult UpdateCart(int productID , int? amount)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Giohang");
            try
            {
                if(cart!= null)
                {
                    CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productID);
                    if(item!=null&& amount.HasValue)
                    {
                        item.amount = amount.Value;

                    }
                    HttpContext.Session.Set<List<CartItem>>("Giohang", cart);
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }

        }

        [HttpPost]
        [Route("/cart/remove")]
        public IActionResult Remove (int productID)
        {
            try
            {
                List<CartItem> gioHang = Giohang;
                CartItem item = gioHang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                HttpContext.Session.Set<List<CartItem>>("Giohang", gioHang);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }


        }
        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
         /*   List<int> lsproducts = new List<int>();*/
/*            var lsgiohang = Giohang;*/
          /*  foreach(var item in lsgiohang)
            {
                lsproducts.Add(item.product.ProductId);
            }*/
/*            List<Products> lsProduct = _context.Products
                .OrderByDescending(x => x.ProductId)
                .Where(x => x.BestSellers == true && !lsproducts.Contains(x.ProductId))
                .Take(6)
                .ToList();
            ViewBag.lssanpham = lsProduct;*/

             return View(Giohang);
        }
    }
}
