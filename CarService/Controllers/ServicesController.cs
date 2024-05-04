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
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Services
        [Authorize(Roles = "Admin,StandardUser")]

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Services.Include(s => s.Mechanic).Include(s => s.Vehicle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Services/Details/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Mechanic)
                .Include(s => s.Vehicle)
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            ViewData["MechanicID"] = new SelectList(_context.Mechanics, "ID", "FName");
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "VehicleID", "VinNumber");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceID,ServiceDate,Description,ServiceCost,MechanicID,VehicleID")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MechanicID"] = new SelectList(_context.Mechanics, "ID", "FName", service.MechanicID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "VehicleID", "VinNumber", service.VehicleID);
            return View(service);
        }

        // GET: Services/Edit/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["MechanicID"] = new SelectList(_context.Mechanics, "ID", "FName", service.MechanicID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "VehicleID", "VinNumber", service.VehicleID);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, [Bind("ServiceID,ServiceDate,Description,ServiceCost,MechanicID,VehicleID")] Service service)
        {
            if (id != service.ServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceID))
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
            ViewData["MechanicID"] = new SelectList(_context.Mechanics, "ID", "FName", service.MechanicID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "VehicleID", "VinNumber", service.VehicleID);
            return View(service);
        }

        // GET: Services/Delete/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Mechanic)
                .Include(s => s.Vehicle)
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return _context.Services.Any(e => e.ServiceID == id);
        }
    }
}
