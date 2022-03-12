using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HShopping.Models;
using PagedList.Core;
using HShopping.Helpers;
using System.IO;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace HShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly dbHshoppingContext _context;
        public INotyfService _notyfService { get; }
        public ProductsController(dbHshoppingContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Products
        public IActionResult Index(int page =1 , int CatID=0 )
        {
            var pagenumber = page;
            var pageSize = 10;
            List<Products> lsproducts = new List<Products>();
            if(CatID != 0)
            {
                lsproducts =_context.Products
                    .AsNoTracking()
                    .Where(x=>x.CatId == CatID)
                    .Include(x => x.Cat)
                    .OrderByDescending(x => x.ProductId).ToList();
            } else
            {
                lsproducts = _context.Products
                    .AsNoTracking()
                    .Include(x => x.Cat)
                    .OrderByDescending(x => x.ProductId).ToList();
            }    
             
            PagedList<Products> model = new PagedList<Products>(lsproducts.AsQueryable(), pagenumber, pageSize);
            ViewBag.CurrentPage = pagenumber;
            ViewBag.CurrentCatID = CatID;
            ViewData["Danhmucsanpham"] = new SelectList(_context.Categories, "CatId", "CatName",CatID);

            return View(model);
        }
        public IActionResult Filtter(int CatID = 0)
        {
            var url = $"/Admin/Products?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/Products";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["Danhmucsanpham"] = new SelectList(_context.Categories, "CatId", "CatName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,DateCreated,DateModified,BestSellers,Active,Tags,Title,UnitsInStock,Thumb")] Products products, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                products.ProductName = Utilities.ToTitleCase(products.ProductName);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(products.ProductName) + extension;
                    products.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());

                }
                if (string.IsNullOrEmpty(products.Thumb)) products.Thumb = "default.jpg";
                products.DateModified = DateTime.Now;
                products.DateCreated = DateTime.Now;
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Danhmucsanpham"] = new SelectList(_context.Categories, "CatId", "CatName", products.CatId);
            _notyfService.Success("Tạo mới thành công");
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["Danhmucsanpham"] = new SelectList(_context.Categories, "CatId", "CatName", products.CatId);
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ShortDesc,Description,CatId,Price,Discount,DateCreated,DateModified,BestSellers,Active,Tags,Title,UnitsInStock,Thumb")] Products products, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    products.ProductName = Utilities.ToTitleCase(products.ProductName);
                    if(fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(products.ProductName) + extension;
                        products.Thumb = await Utilities.UploadFile(fThumb, @"products", image.ToLower());

                    }
                    if (string.IsNullOrEmpty(products.Thumb)) products.Thumb = "default.jpg";
                       products.DateModified = DateTime.Now;
                    _context.Update(products);
                    _notyfService.Success("Cập nhật thành công");
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductId))
                    {
                        _notyfService.Success("Có lỗi xảy ra");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Danhmucsanpham"] = new SelectList(_context.Categories, "CatId", "CatName", products.CatId);
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Cat)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
