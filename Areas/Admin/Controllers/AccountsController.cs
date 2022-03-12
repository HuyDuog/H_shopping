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

namespace HShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        private readonly dbHshoppingContext _context;
        public INotyfService _notyfService { get; }
        public AccountsController(dbHshoppingContext context , INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Admin/Accounts
        public IActionResult Index(int page = 1, int RoleId = 0)
        {
            var pagenumber = page;
            var pageSize = 10;
            ViewData["QuyenTruyCap"] = new SelectList(_context.Roles, "RoleId", "Description", RoleId);
            List<SelectListItem> isStatus = new List<SelectListItem>();
            isStatus.Add(new SelectListItem() { Text = "Active ", Value = "1" });
            isStatus.Add(new SelectListItem() { Text = "Block ", Value = "0" });
            ViewData["isStatus"] = isStatus;
            List<Accounts> lsaccounts = new List<Accounts>();
            if (RoleId != 0)
            {
                lsaccounts = _context.Accounts
                    .AsNoTracking()
                    .Where(x => x.RoleId == RoleId)
                    .Include(x => x.Role)
                    .OrderByDescending(x => x.RoleId).ToList();
            }
            else
            {
                lsaccounts = _context.Accounts
                    .AsNoTracking()
                    .Include(x => x.Role)
                    .OrderByDescending(x => x.RoleId).ToList();
            }
            PagedList<Accounts> model = new PagedList<Accounts>(lsaccounts.AsQueryable(), pagenumber, pageSize);
            ViewBag.CurrentPage = pagenumber;
            ViewBag.CurrentCatID = RoleId;

            return View(model);
        }
        public IActionResult Filter(int RoleId = 0)
        {
            var url = $"/Admin/Accounts?RoleId={RoleId}";
            if (RoleId == 0)
            {
                url = $"/Admin/Accounts";
            }
            return Json(new { status = "success", redirectUrl = url });
        }
        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Phone,Email,Password,Active,FullName,RoleId,LastLogin,CreateDate")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accounts);
                await _context.SaveChangesAsync();
                _notyfService.Success("Tạo mới thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", accounts.RoleId);
            return View(accounts);
        }

        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts.FindAsync(id);
            if (accounts == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", accounts.RoleId);
            return View(accounts);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,Password,Active,FullName,RoleId,LastLogin,CreateDate")] Accounts accounts)
        {
            if (id != accounts.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accounts);
                    _notyfService.Success("Cập nhật thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountsExists(accounts.AccountId))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", accounts.RoleId);
            return View(accounts);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var accounts = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accounts = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(accounts);
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool AccountsExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

    }

}
