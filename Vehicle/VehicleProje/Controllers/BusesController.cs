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
    public class BusesController : Controller
    {
        private readonly VehicleContext _context;

        public BusesController(VehicleContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            var vehicleContext = _context.Buses.Include(b => b.Color);
            return View(await vehicleContext.ToListAsync());
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Buses == null)
            {
                return NotFound();
            }

            var bus = await _context.Buses
                .Include(b => b.Color)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: Buses/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName");
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,ColorId")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", bus.ColorId);
            return View(bus);
        }

        // GET: Buses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Buses == null)
            {
                return NotFound();
            }

            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", bus.ColorId);
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,ColorId")] Bus bus)
        {
            if (id != bus.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusExists(bus.VehicleId))
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
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName", bus.ColorId);
            return View(bus);
        }

        // GET: Buses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Buses == null)
            {
                return NotFound();
            }

            var bus = await _context.Buses
                .Include(b => b.Color)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Buses == null)
            {
                return Problem("Entity set 'VehicleContext.Buses'  is null.");
            }
            var bus = await _context.Buses.FindAsync(id);
            if (bus != null)
            {
                _context.Buses.Remove(bus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(int id)
        {
          return (_context.Buses?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
