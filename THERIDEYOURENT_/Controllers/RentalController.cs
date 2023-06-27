using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using THERIDEYOURENT_.Models;

namespace THERIDEYOURENT_.Controllers
{
    public class RentalController : Controller
    {
        private readonly TheRideYouRentPoe3Context _context;

        public RentalController(TheRideYouRentPoe3Context context)
        {
            _context = context;
        }

        // GET: Rental
        public async Task<IActionResult> Index()
        {
            var theRideYouRentPoe3Context = _context.Rentals.Include(r => r.CarNoNavigation).Include(r => r.Driver).Include(r => r.InspectorNoNavigation);
            return View(await theRideYouRentPoe3Context.ToListAsync());
        }

        // GET: Rental/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.CarNoNavigation)
                .Include(r => r.Driver)
                .Include(r => r.InspectorNoNavigation)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rental/Create
        public IActionResult Create()
        {
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarId", "CarNo");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name");
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorId", "Name");
            return View();
        }

        // POST: Rental/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalId,CarNo,InspectorNo,DriverId,RentalFee,StartDate,EndDate")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarId", "CarNo", rental.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", rental.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorId", "Name", rental.InspectorNo);
            return View(rental);
        }

        // GET: Rental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarId", "CarNo", rental.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", rental.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorId", "Name", rental.InspectorNo);
            return View(rental);
        }

        // POST: Rental/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalId,CarNo,InspectorNo,DriverId,RentalFee,StartDate,EndDate")] Rental rental)
        {
            if (id != rental.RentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalId))
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
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarId", "CarNo", rental.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", rental.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorId", "Name", rental.InspectorNo);
            return View(rental);
        }

        // GET: Rental/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.CarNoNavigation)
                .Include(r => r.Driver)
                .Include(r => r.InspectorNoNavigation)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rentals == null)
            {
                return Problem("Entity set 'TheRideYouRentPoe3Context.Rentals'  is null.");
            }
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
          return (_context.Rentals?.Any(e => e.RentalId == id)).GetValueOrDefault();
        }
    }
}
