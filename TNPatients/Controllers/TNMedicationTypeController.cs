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
    /// This class reads data through MedicationType model (using Entity Framework), 
    /// selects the appropriate view
    /// passes data to the TNMedicationType View
    /// receives <form> data via HTML POST and writes to MedicationType table in database through the model
    /// </summary>
    public class TNMedicationTypeController : Controller
    {
        private readonly PatientsContext _context;

        // Method to initialize the data context through dependency injection
        public TNMedicationTypeController(PatientsContext context)
        {
            _context = context;
        }

        // GET: TNMedicationType
        // Index method returns a view with the list of all records in the table
        public async Task<IActionResult> Index()
        {
              return View(await _context.MedicationTypes.ToListAsync());
        }

        // GET: TNMedicationType/Details/5
        // Details method returns a view with all fields for the selected record (key = "id"). If that id doesn't exit, it returns not found page
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MedicationTypes == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationTypes
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // GET: TNMedicationType/Create
        // Create(GET) method returns a view to display a blank input page
        public IActionResult Create()
        {
            return View();
        }

        // POST: TNMedicationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Create(POST) adds the new record to the table in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Create([Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicationType);
        }

        // GET: TNMedicationType/Edit/5
        // Edit (GET) method returns a view to display the selected record for updating
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedicationTypes == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationTypes.FindAsync(id);
            if (medicationType == null)
            {
                return NotFound();
            }
            return View(medicationType);
        }

        // POST: TNMedicationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Edit (POST) saves the updated record in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Edit(int id, [Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            if (id != medicationType.MedicationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationTypeExists(medicationType.MedicationTypeId))
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
            return View(medicationType);
        }

        // GET: TNMedicationType/Delete/5
        // Delete (GET) method returns a view to display the selected record to confirm the delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedicationTypes == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationTypes
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // POST: TNMedicationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // DeleteConfirmed (POST) deletes the selected record from DB and redirect to Index page if data is valid
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedicationTypes == null)
            {
                return Problem("Entity set 'PatientsContext.MedicationTypes'  is null.");
            }
            var medicationType = await _context.MedicationTypes.FindAsync(id);
            if (medicationType != null)
            {
                _context.MedicationTypes.Remove(medicationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ConcentrationUnitExists method checks if there is any record in the table that matches the passed id
        private bool MedicationTypeExists(int id)
        {
          return _context.MedicationTypes.Any(e => e.MedicationTypeId == id);
        }
    }
}
