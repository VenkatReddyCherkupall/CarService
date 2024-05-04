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
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicles.Include(v => v.Customer);
            var sessionClicks = HttpContext.Session.Get<List<Vehicle>>("UserActClicks");
            if (sessionClicks != null)
            {
                ViewBag.UserActClicks = sessionClicks;
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Customer)
                .Include(v => v.Services).ThenInclude(v => v.Mechanic)
                .FirstOrDefaultAsync(m => m.VehicleID == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            AddClickedActToSession(vehicle);

            return View(vehicle);
        }

        // GET: Vehicles/Create
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "FName");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([Bind("VehicleID,VinNumber,Make,Model,Year,Color,CustomerID")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "FName", vehicle.CustomerID);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "FName", vehicle.CustomerID);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, [Bind("VehicleID,VinNumber,Make,Model,Year,Color,CustomerID")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customers, "ID", "FName", vehicle.CustomerID);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(m => m.VehicleID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehicles'  is null.");
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return _context.Vehicles.Any(e => e.VehicleID == id);
        }


        //Get method for parital view
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DetailsAjax(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(m => m.VehicleID == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            AddClickedActToSession(vehicle);
            return View(vehicle);
        }
        //HTTPPOST method for parital view

        [HttpPost]
        public IActionResult GetVehicleServices(int id)
        {
            var vehicle = _context.Vehicles.Include(m => m.Services).ThenInclude(e => e.PartsUsed)
                                                        .Include(m => m.Services).ThenInclude(e => e.Mechanic)
                                                        .FirstOrDefault(m => m.VehicleID == id);


            return PartialView("_VehicleServices", vehicle.Services.ToList());
        }

        //Session method named AddClickedActToSession
        private void AddClickedActToSession(Vehicle act)
        {
            var sessionClicks = HttpContext.Session.Get<List<Vehicle>>("UserActClicks");

            if (sessionClicks == null)
            {
                sessionClicks = new List<Vehicle>();
            }

            var actInSession = sessionClicks.FirstOrDefault(m => m.VehicleID == act.VehicleID);

            if (actInSession == null)
            {
                sessionClicks.Add(act);
                HttpContext.Session.Set("UserActClicks", sessionClicks);
            }
        }


    }
}
