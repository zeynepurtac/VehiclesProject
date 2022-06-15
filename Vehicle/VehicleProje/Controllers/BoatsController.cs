using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleProje.Models;

namespace VehicleProje.Controllers
{
    public class BoatsController : Controller
    {
        private readonly VehicleContext _context;

        public BoatsController(VehicleContext context)
        {
            _context = context;
        }

        // GET: Boats
        public async Task<IActionResult> Index()
        {
            var vehicleContext = _context.Boats.Include(b => b.Color);
            return View(await vehicleContext.ToListAsync());
        }

        // GET: Boats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Boats == null)
            {
                return NotFound();
            }

            var boat = await _context.Boats
                .Include(b => b.Color)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (boat == null)
            {
                return NotFound();
            }

            return View(boat);
        }

        // GET: Boats/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName");
            return View();
        }

        // POST: Boats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,ColorId")] Boat boat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", boat.ColorId);
            return View(boat);
        }

        // GET: Boats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Boats == null)
            {
                return NotFound();
            }

            var boat = await _context.Boats.FindAsync(id);
            if (boat == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", boat.ColorId);
            return View(boat);
        }

        // POST: Boats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,ColorId")] Boat boat)
        {
            if (id != boat.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoatExists(boat.VehicleId))
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
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", boat.ColorId);
            return View(boat);
        }

        // GET: Boats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Boats == null)
            {
                return NotFound();
            }

            var boat = await _context.Boats
                .Include(b => b.Color)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (boat == null)
            {
                return NotFound();
            }

            return View(boat);
        }

        // POST: Boats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Boats == null)
            {
                return Problem("Entity set 'VehicleContext.Boats'  is null.");
            }
            var boat = await _context.Boats.FindAsync(id);
            if (boat != null)
            {
                _context.Boats.Remove(boat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoatExists(int id)
        {
          return (_context.Boats?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
