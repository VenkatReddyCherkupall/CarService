/*VenkatReddy Cherkupalli*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarService.Data;
using CarService.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CarService.Controllers
{
    public class PartsUsedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartsUsedsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PartsUseds
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PartsUsed.Include(p => p.Service);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PartsUseds/Details/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PartsUsed == null)
            {
                return NotFound();
            }

            var partsUsed = await _context.PartsUsed
                .Include(p => p.Service)
                .FirstOrDefaultAsync(m => m.PartsUsedID == id);
            if (partsUsed == null)
            {
                return NotFound();
            }

            return View(partsUsed);
        }

        // GET: PartsUseds/Create
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceID");
            return View();
        }

        // POST: PartsUseds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([Bind("PartsUsedID,PartName,PartNumber,Cost,Description,ServiceID")] PartsUsed partsUsed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partsUsed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceID", partsUsed.ServiceID);
            return View(partsUsed);
        }

        // GET: PartsUseds/Edit/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PartsUsed == null)
            {
                return NotFound();
            }

            var partsUsed = await _context.PartsUsed.FindAsync(id);
            if (partsUsed == null)
            {
                return NotFound();
            }
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceID", partsUsed.ServiceID);
            return View(partsUsed);
        }

        // POST: PartsUseds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, [Bind("PartsUsedID,PartName,PartNumber,Cost,Description,ServiceID")] PartsUsed partsUsed)
        {
            if (id != partsUsed.PartsUsedID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partsUsed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartsUsedExists(partsUsed.PartsUsedID))
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
            ViewData["ServiceID"] = new SelectList(_context.Services, "ServiceID", "ServiceID", partsUsed.ServiceID);
            return View(partsUsed);
        }

        // GET: PartsUseds/Delete/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PartsUsed == null)
            {
                return NotFound();
            }

            var partsUsed = await _context.PartsUsed
                .Include(p => p.Service)
                .FirstOrDefaultAsync(m => m.PartsUsedID == id);
            if (partsUsed == null)
            {
                return NotFound();
            }

            return View(partsUsed);
        }

        // POST: PartsUseds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PartsUsed == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PartsUsed'  is null.");
            }
            var partsUsed = await _context.PartsUsed.FindAsync(id);
            if (partsUsed != null)
            {
                _context.PartsUsed.Remove(partsUsed);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartsUsedExists(int id)
        {
          return _context.PartsUsed.Any(e => e.PartsUsedID == id);
        }
    }
}
