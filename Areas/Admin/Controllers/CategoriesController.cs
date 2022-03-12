using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HShopping.Models;
using PagedList.Core;
using AspNetCoreHero.ToastNotification.Abstractions;
using HShopping.Helpers;
using System.IO;

namespace HShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly dbHshoppingContext _context;
        public INotyfService _notyfService { get; }
        public CategoriesController(dbHshoppingContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index(int page = 1, int CatID = 0)
        {
            var pagenumber = page;
            var pageSize = 10;
            ViewData["Category"] = new SelectList(_context.Categories, "CatId", "CatName", CatID);
            List<Categories> lsaccounts = new List<Categories>();
            if (CatID != 0)
            {
                lsaccounts = _context.Categories
                    .AsNoTracking()
                    .Where(x => x.CatId == CatID)
                    .OrderByDescending(x => x.CatId).ToList();
            }
            else
            {
                lsaccounts = _context.Categories
                    .AsNoTracking()
                    .OrderByDescending(x => x.CatId).ToList();
            }
            PagedList<Categories> model = new PagedList<Categories>(lsaccounts.AsQueryable(), pagenumber, pageSize);
            ViewBag.CurrentPage = pagenumber;
            ViewBag.CurrentCatID = CatID;

            return View(model);
        }
        public IActionResult Filtter(int CatID = 0)
        {
            var url = $"/Admin/Categories?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/Categories";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,CatName,Description,Levels,Title,Thumb")] Categories categories, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                categories.CatName = Utilities.ToTitleCase(categories.CatName);
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(categories.CatName) + extension;
                    categories.Thumb = await Utilities.UploadFile(fThumb, @"category", image.ToLower());

                }
                if (string.IsNullOrEmpty(categories.Thumb)) categories.Thumb = "default.jpg";
                _context.Add(categories);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            _notyfService.Success("Tạo mới thành công");
            return View(categories);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatId,CatName,Description,Levels,Title,Thumb")] Categories categories, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != categories.CatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    categories.CatName = Utilities.ToTitleCase(categories.CatName);
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string image = Utilities.SEOUrl(categories.CatName) + extension;
                        categories.Thumb = await Utilities.UploadFile(fThumb, @"category", image.ToLower());

                    }
                    if (string.IsNullOrEmpty(categories.Thumb)) categories.Thumb = "default.jpg";
                    _context.Update(categories);
                    _notyfService.Success("Cập nhật thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.CatId))
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
            return View(categories);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories
                .FirstOrDefaultAsync(m => m.CatId == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.CatId == id);
        }
    }
}
