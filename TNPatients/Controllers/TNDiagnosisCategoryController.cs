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
    /// This class reads data through DiagnosisCategory model (using Entity Framework), 
    /// selects the appropriate view
    /// passes data to the TNDiagnosisCategory View
    /// receives <form> data via HTML POST and writes to DiagnosisCategory table in database through the model
    /// </summary>
    public class TNDiagnosisCategoryController : Controller
    {
        private readonly PatientsContext _context;

        // Method to initialize the data context through dependency injection
        public TNDiagnosisCategoryController(PatientsContext context)
        {
            _context = context;
        }

        // GET: TNDiagnosisCategory
        // Index method returns a view with the list of all records in the table
        public async Task<IActionResult> Index()
        {
              return View(await _context.DiagnosisCategories.ToListAsync());
        }

        // GET: TNDiagnosisCategory/Details/5
        // Details method returns a view with all fields for the selected record (key = "id"). If that id doesn't exit, it returns not found page
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DiagnosisCategories == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }

            return View(diagnosisCategory);
        }

        // GET: TNDiagnosisCategory/Create
        // Create(GET) method returns a view to display a blank input page
        public IActionResult Create()
        {
            return View();
        }

        // POST: TNDiagnosisCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Create(POST) adds the new record to the table in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Create([Bind("Id,Name")] DiagnosisCategory diagnosisCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnosisCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnosisCategory);
        }

        // GET: TNDiagnosisCategory/Edit/5
        // Edit (GET) method returns a view to display the selected record for updating
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DiagnosisCategories == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategories.FindAsync(id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }
            return View(diagnosisCategory);
        }

        // POST: TNDiagnosisCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Edit (POST) saves the updated record in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DiagnosisCategory diagnosisCategory)
        {
            if (id != diagnosisCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnosisCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosisCategoryExists(diagnosisCategory.Id))
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
            return View(diagnosisCategory);
        }

        // GET: TNDiagnosisCategory/Delete/5
        // Delete (GET) method returns a view to display the selected record to confirm the delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DiagnosisCategories == null)
            {
                return NotFound();
            }

            var diagnosisCategory = await _context.DiagnosisCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnosisCategory == null)
            {
                return NotFound();
            }

            return View(diagnosisCategory);
        }

        // POST: TNDiagnosisCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // DeleteConfirmed (POST) deletes the selected record from DB and redirect to Index page if data is valid
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DiagnosisCategories == null)
            {
                return Problem("Entity set 'PatientsContext.DiagnosisCategories'  is null.");
            }
            var diagnosisCategory = await _context.DiagnosisCategories.FindAsync(id);
            if (diagnosisCategory != null)
            {
                _context.DiagnosisCategories.Remove(diagnosisCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DiagnosisCategoryExists method checks if there is any record in the table that matches the passed id
        private bool DiagnosisCategoryExists(int id)
        {
          return _context.DiagnosisCategories.Any(e => e.Id == id);
        }
    }
}
