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
    public class TNConcentrationUnitController : Controller
    {
        private readonly PatientsContext _context;

        // Method to initialize the data context through dependency injection
        public TNConcentrationUnitController(PatientsContext context)
        {
            _context = context;
        }

        // GET: TNConcentrationUnit
        // Index method returns a view with the list of all records in the table
        public async Task<IActionResult> Index()
        {
              return View(await _context.ConcentrationUnits.ToListAsync());
        }

        // GET: TNConcentrationUnit/Details/5
        // Details method returns a view with all fields for the selected record (key = "id"). If that id doesn't exit, it returns not found page
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ConcentrationUnits == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnits
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }

            return View(concentrationUnit);
        }

        // GET: TNConcentrationUnit/Create
        // Create(GET) method returns a view to display a blank input page
        public IActionResult Create()
        {
            return View();
        }

        // POST: TNConcentrationUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Create(POST) adds the new record to the table in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Create([Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concentrationUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concentrationUnit);
        }

        // GET: TNConcentrationUnit/Edit/5
        // Edit (GET) method returns a view to display the selected record for updating
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ConcentrationUnits == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnits.FindAsync(id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }
            return View(concentrationUnit);
        }

        // POST: TNConcentrationUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Edit (POST) saves the updated record in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Edit(string id, [Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            if (id != concentrationUnit.ConcentrationCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concentrationUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcentrationUnitExists(concentrationUnit.ConcentrationCode))
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
            return View(concentrationUnit);
        }

        // GET: TNConcentrationUnit/Delete/5
        // Delete (GET) method returns a view to display the selected record to confirm the delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ConcentrationUnits == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnits
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }

            return View(concentrationUnit);
        }

        // POST: TNConcentrationUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // DeleteConfirmed (POST) deletes the selected record from DB and redirect to Index page if data is valid
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ConcentrationUnits == null)
            {
                return Problem("Entity set 'PatientsContext.ConcentrationUnits'  is null.");
            }
            var concentrationUnit = await _context.ConcentrationUnits.FindAsync(id);
            if (concentrationUnit != null)
            {
                _context.ConcentrationUnits.Remove(concentrationUnit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ConcentrationUnitExists method checks if there is any record in the table that matches the passed id
        private bool ConcentrationUnitExists(string id)
        {
          return _context.ConcentrationUnits.Any(e => e.ConcentrationCode == id);
        }
    }
}
