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
    public class ReturnController : Controller
    {
        private readonly TheRideYouRentPoe3Context _context;

        public ReturnController(TheRideYouRentPoe3Context context)
        {
            _context = context;
        }

        // GET: Return
        public async Task<IActionResult> Index()
        {
            var theRideYouRentPoe3Context = _context.Returns.Include(r => r.CarNoNavigation)
                                                           .Include(r => r.Driver)
                                                           .Include(r => r.InspectorNoNavigation);

         //   var driverFines = await _context.CalculateDriverFine.ToListAsync();

            // Pass the driver fines data to the view
        //    ViewBag.DriverFines = driverFines;

            return View(await theRideYouRentPoe3Context.ToListAsync());
        }

        // GET: Return/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Returns == null)
            {
                return NotFound();
            }

            var @return = await _context.Returns.Include(r => r.CarNoNavigation)
                                                .Include(r => r.Driver)
                                                .Include(r => r.InspectorNoNavigation)
                                                .FirstOrDefaultAsync(m => m.ReturnId == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // GET: Return/Create
        public IActionResult Create()
        {
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarId", "CarNo");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name");
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorId", "Name");
            return View();
        }

        // POST: Return/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReturnId,CarNo,InspectorNo,DriverId,ReturnDate,ElapsedDate,Fine")] Return @return)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@return);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarId", "CarNo", @return.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", @return.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorId", "Name", @return.InspectorNo);
            return View(@return);
        }

        // GET: Return/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Returns == null)
            {
                return NotFound();
            }

            var @return = await _context.Returns.FindAsync(id);
            if (@return == null)
            {
                return NotFound();
            }
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarId", "CarNo", @return.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", @return.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorId", "Name", @return.InspectorNo);
            return View(@return);
        }

        // POST: Return/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReturnId,CarNo,InspectorNo,DriverId,ReturnDate,ElapsedDate,Fine")] Return @return)
        {
            if (id != @return.ReturnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@return);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnExists(@return.ReturnId))
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
            ViewData["CarNo"] = new SelectList(_context.Cars, "CarId", "CarNo", @return.CarNo);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "Name", @return.DriverId);
            ViewData["InspectorNo"] = new SelectList(_context.Inspectors, "InspectorId", "Name", @return.InspectorNo);
            return View(@return);
        }

        // GET: Return/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Returns == null)
            {
                return NotFound();
            }

            var @return = await _context.Returns.Include(r => r.CarNoNavigation)
                                                .Include(r => r.Driver)
                                                .Include(r => r.InspectorNoNavigation)
                                                .FirstOrDefaultAsync(m => m.ReturnId == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // POST: Return/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @return = await _context.Returns.FindAsync(id);
            if (@return != null)
            {
                _context.Returns.Remove(@return);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnExists(int id)
        {
            return _context.Returns.Any(e => e.ReturnId == id);
        }




        // GET: Return/CalculateDriverFine
        public IActionResult CalculateDriverFine()
        {
            return View();
        }

        // POST: Return/CalculateDriverFine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CalculateDriverFine(int driverId)
        {
            var driver = _context.Drivers.FirstOrDefault(d => d.DriverId == driverId);
            if (driver == null)
            {
                ModelState.AddModelError("DriverId", "Invalid driver ID");
                return View();
            }

            var totalFine = _context.Returns
                .Where(r => r.DriverId == driverId)
                .Sum(r => r.ElapsedDate * 500);

            ViewBag.DriverFine = totalFine;
            ViewBag.DriverName = driver.Name;

            return View();
        }




    }
}
