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
    /// This class reads data through Country model (using Entity Framework), 
    /// selects the appropriate view
    /// passes data to the TNCountry View
    /// receives <form> data via HTML POST and writes to Country table in database through the model
    /// </summary>
    public class TNCountryController : Controller
    {
        private readonly PatientsContext _context;

        // Method to initialize the data context through dependency injection
        public TNCountryController(PatientsContext context)
        {
            _context = context;
        }

        // GET: TNCountry
        // Index method returns a view with the list of all records in the table
        public async Task<IActionResult> Index()
        {
              return View(await _context.Countries.ToListAsync());
        }

        // GET: TNCountry/Details/5
        // Details method returns a view with all fields for the selected record (key = "id"). If that id doesn't exit, it returns not found page
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: TNCountry/Create
        // Create(GET) method returns a view to display a blank input page
        public IActionResult Create()
        {
            return View();
        }

        // POST: TNCountry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Create(POST) adds the new record to the table in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Create([Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: TNCountry/Edit/5
        // Edit (GET) method returns a view to display the selected record for updating
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: TNCountry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Edit (POST) saves the updated record in DB and redirect to Index page if data is valid
        public async Task<IActionResult> Edit(string id, [Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            if (id != country.CountryCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryCode))
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
            return View(country);
        }

        // GET: TNCountry/Delete/5
        // Delete (GET) method returns a view to display the selected record to confirm the delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: TNCountry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // DeleteConfirmed (POST) deletes the selected record from DB and redirect to Index page if data is valid
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'PatientsContext.Countries'  is null.");
            }
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // CountryExists method checks if there is any record in the table that matches the passed id
        private bool CountryExists(string id)
        {
          return _context.Countries.Any(e => e.CountryCode == id);
        }
    }
}
