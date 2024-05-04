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
using CarService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CarService.Controllers
{
    public class MechanicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MechanicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mechanics
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Index()
        {
              return View(await _context.Mechanics.ToListAsync());
        }

        // GET: Mechanics/Details/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mechanics == null)
            {
                return NotFound();
            }

            var mechanic = await _context.Mechanics
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mechanic == null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        // GET: Mechanics/Create
        [Authorize(Roles = "Admin")]


        public IActionResult Create()
        {
            return View();
        }

        // POST: Mechanics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([Bind("Experience,Speciality,StartDate,PayRate,ID,FName,LName,Phone,Email,Street,City,State,ZipCode")] Mechanic mechanic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mechanic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mechanic);
        }

        // GET: Mechanics/Edit/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mechanics == null)
            {
                return NotFound();
            }

            var mechanic = await _context.Mechanics.FindAsync(id);
            if (mechanic == null)
            {
                return NotFound();
            }
            return View(mechanic);
        }

        // POST: Mechanics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, [Bind("Experience,Speciality,StartDate,PayRate,ID,FName,LName,Phone,Email,Street,City,State,ZipCode")] Mechanic mechanic)
        {
            if (id != mechanic.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mechanic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MechanicExists(mechanic.ID))
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
            return View(mechanic);
        }

        // GET: Mechanics/Delete/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mechanics == null)
            {
                return NotFound();
            }

            var mechanic = await _context.Mechanics
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mechanic == null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        // POST: Mechanics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mechanics == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mechanics'  is null.");
            }
            var mechanic = await _context.Mechanics.FindAsync(id);
            if (mechanic != null)
            {
                _context.Mechanics.Remove(mechanic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MechanicExists(int id)
        {
          return _context.Mechanics.Any(e => e.ID == id);
        }

        

    }
}
