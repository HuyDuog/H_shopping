using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HShopping.Models;

namespace HShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransactStatusController : Controller
    {
        private readonly dbHshoppingContext _context;

        public TransactStatusController(dbHshoppingContext context)
        {
            _context = context;
        }

        // GET: Admin/TransactStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransactStatus.ToListAsync());
        }

        // GET: Admin/TransactStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactStatus = await _context.TransactStatus
                .FirstOrDefaultAsync(m => m.TransactStatusId == id);
            if (transactStatus == null)
            {
                return NotFound();
            }

            return View(transactStatus);
        }

        // GET: Admin/TransactStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TransactStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactStatusId,Status,Description")] TransactStatus transactStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactStatus);
        }

        // GET: Admin/TransactStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactStatus = await _context.TransactStatus.FindAsync(id);
            if (transactStatus == null)
            {
                return NotFound();
            }
            return View(transactStatus);
        }

        // POST: Admin/TransactStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactStatusId,Status,Description")] TransactStatus transactStatus)
        {
            if (id != transactStatus.TransactStatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactStatusExists(transactStatus.TransactStatusId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transactStatus);
        }

        // GET: Admin/TransactStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactStatus = await _context.TransactStatus
                .FirstOrDefaultAsync(m => m.TransactStatusId == id);
            if (transactStatus == null)
            {
                return NotFound();
            }

            return View(transactStatus);
        }

        // POST: Admin/TransactStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactStatus = await _context.TransactStatus.FindAsync(id);
            _context.TransactStatus.Remove(transactStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactStatusExists(int id)
        {
            return _context.TransactStatus.Any(e => e.TransactStatusId == id);
        }
    }
}
