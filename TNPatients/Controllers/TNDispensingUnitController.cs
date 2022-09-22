using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TNPatients.Models;

namespace TNPatients.Controllers
{
    /// <summary>
    /// This class reads data through ConcentrationUnit model (using Entity Framework), 
    /// selects the appropriate view
    /// passes data to the TNConcentrationUnit View
    /// receives <form> data via HTML POST and writes to ConcentrationUnit table in database through the model
    /// </summary>
    public class TNDispensingUnitController : Controller
    {
        private readonly PatientsContext _context;

        // Method to initialize the data context through dependency injection
        public TNDispensingUnitController(PatientsContext context)
        {
            _context = context;
        }

        // GET: TNDispensingUnit
        // Index method returns a view with the list of all records in the table
        public async Task<IActionResult> Index()
        {
              return View(await _context.DispensingUnits.ToListAsync());
        }

        // GET: TNDispensingUnit/Details/5
        // Details method returns a view with all fields for the selected record (key = "id"). If that id doesn't exit, it returns not found page
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DispensingUnits == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnits
                .FirstOrDefaultAsync(m => m.DispensingCode == id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }

            return View(dispensingUnit);
        }

        // GET: TNDispensingUnit/Create
        // Create(GET) method returns a view to display a blank input page
        public IActionResult Create()
        {
            return View();
        }

        // POST: TNDispensingUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Create(POST) adds the new record to the table in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Create([Bind("DispensingCode")] DispensingUnit dispensingUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dispensingUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dispensingUnit);
        }

        // GET: TNDispensingUnit/Edit/5
        // Edit (GET) method returns a view to display the selected record for updating
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DispensingUnits == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnits.FindAsync(id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }
            return View(dispensingUnit);
        }

        // POST: TNDispensingUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Edit (POST) saves the updated record in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Edit(string id, [Bind("DispensingCode")] DispensingUnit dispensingUnit)
        {
            if (id != dispensingUnit.DispensingCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispensingUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DispensingUnitExists(dispensingUnit.DispensingCode))
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
            return View(dispensingUnit);
        }

        // GET: TNDispensingUnit/Delete/5
        // Delete (GET) method returns a view to display the selected record to confirm the delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DispensingUnits == null)
            {
                return NotFound();
            }

            var dispensingUnit = await _context.DispensingUnits
                .FirstOrDefaultAsync(m => m.DispensingCode == id);
            if (dispensingUnit == null)
            {
                return NotFound();
            }

            return View(dispensingUnit);
        }

        // POST: TNDispensingUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // DeleteConfirmed (POST) deletes the selected record from DB and redirect to Index page if data is valid
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DispensingUnits == null)
            {
                return Problem("Entity set 'PatientsContext.DispensingUnits'  is null.");
            }
            var dispensingUnit = await _context.DispensingUnits.FindAsync(id);
            if (dispensingUnit != null)
            {
                _context.DispensingUnits.Remove(dispensingUnit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DispensingUnitExists method checks if there is any record in the table that matches the passed id
        private bool DispensingUnitExists(string id)
        {
          return _context.DispensingUnits.Any(e => e.DispensingCode == id);
        }
    }
}
