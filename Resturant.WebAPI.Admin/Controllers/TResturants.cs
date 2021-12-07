using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resturant.DBModels.Entities;
using Resturant.DataAccess.Context;

namespace Resturant.WebAPI.Admin.Controllers
{
    public class TResturants : Controller
    {
        private readonly ResturantContext _context;

        public TResturants(ResturantContext context)
        {
            _context = context;
        }

        // GET: TResturants
        public async Task<IActionResult> Index()
        {
            var resturantContext = _context.Resturants.Include(r => r.ResturantTypeNavigation);
            return View(await resturantContext.ToListAsync());
        }

        // GET: TResturants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resturant = await _context.Resturants
                .Include(r => r.ResturantTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resturant == null)
            {
                return NotFound();
            }

            return View(resturant);
        }

        // GET: TResturants/Create
        public IActionResult Create()
        {
            ViewData["ResturantType"] = new SelectList(_context.FoodTypes, "Id", "Type");
            return View();
        }

        // POST: TResturants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResturantType,ResturantName,Pic,Address,Rate")] Resturant.DBModels.Entities.Resturant resturant)
        {
            if (ModelState.IsValid)
            {
                resturant.Id = Guid.NewGuid();
                _context.Add(resturant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResturantType"] = new SelectList(_context.FoodTypes, "Id", "Type", resturant.ResturantType);
            return View(resturant);
        }

        // GET: TResturants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resturant = await _context.Resturants.FindAsync(id);
            if (resturant == null)
            {
                return NotFound();
            }
            ViewData["ResturantType"] = new SelectList(_context.FoodTypes, "Id", "Type", resturant.ResturantType);
            return View(resturant);
        }

        // POST: TResturants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ResturantType,ResturantName,Pic,Address,Rate")] Resturant.DBModels.Entities.Resturant resturant)
        {
            if (id != resturant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resturant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResturantExists(resturant.Id))
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
            ViewData["ResturantType"] = new SelectList(_context.FoodTypes, "Id", "Type", resturant.ResturantType);
            return View(resturant);
        }

        // GET: TResturants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resturant = await _context.Resturants
                .Include(r => r.ResturantTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resturant == null)
            {
                return NotFound();
            }

            return View(resturant);
        }

        // POST: TResturants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var resturant = await _context.Resturants.FindAsync(id);
            _context.Resturants.Remove(resturant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResturantExists(Guid id)
        {
            return _context.Resturants.Any(e => e.Id == id);
        }
    }
}
