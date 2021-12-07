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
    public class TResturantsFoods : Controller
    {
        private readonly ResturantContext _context;

        public TResturantsFoods(ResturantContext context)
        {
            _context = context;
        }

        // GET: TResturantsFoods
        public async Task<IActionResult> Index()
        {
            var resturantContext = _context.ResturantsFoods.Include(r => r.Resturant).Include(r => r.ResturantNavigation);
            return View(await resturantContext.ToListAsync());
        }

        // GET: TResturantsFoods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resturantsFood = await _context.ResturantsFoods
                .Include(r => r.Resturant)
                .Include(r => r.ResturantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resturantsFood == null)
            {
                return NotFound();
            }

            return View(resturantsFood);
        }

        // GET: TResturantsFoods/Create
        public IActionResult Create()
        {
            ViewData["ResturantId"] = new SelectList(_context.Foods, "Id", "Description");
            ViewData["ResturantId"] = new SelectList(_context.Resturants, "Id", "Address");
            return View();
        }

        // POST: TResturantsFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResturantId,FoodId")] ResturantsFood resturantsFood)
        {
            if (ModelState.IsValid)
            {
                resturantsFood.Id = Guid.NewGuid();
                _context.Add(resturantsFood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResturantId"] = new SelectList(_context.Foods, "Id", "Description", resturantsFood.ResturantId);
            ViewData["ResturantId"] = new SelectList(_context.Resturants, "Id", "Address", resturantsFood.ResturantId);
            return View(resturantsFood);
        }

        // GET: TResturantsFoods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resturantsFood = await _context.ResturantsFoods.FindAsync(id);
            if (resturantsFood == null)
            {
                return NotFound();
            }
            ViewData["ResturantId"] = new SelectList(_context.Foods, "Id", "Description", resturantsFood.ResturantId);
            ViewData["ResturantId"] = new SelectList(_context.Resturants, "Id", "Address", resturantsFood.ResturantId);
            return View(resturantsFood);
        }

        // POST: TResturantsFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ResturantId,FoodId")] ResturantsFood resturantsFood)
        {
            if (id != resturantsFood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resturantsFood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResturantsFoodExists(resturantsFood.Id))
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
            ViewData["ResturantId"] = new SelectList(_context.Foods, "Id", "Description", resturantsFood.ResturantId);
            ViewData["ResturantId"] = new SelectList(_context.Resturants, "Id", "Address", resturantsFood.ResturantId);
            return View(resturantsFood);
        }

        // GET: TResturantsFoods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resturantsFood = await _context.ResturantsFoods
                .Include(r => r.Resturant)
                .Include(r => r.ResturantNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resturantsFood == null)
            {
                return NotFound();
            }

            return View(resturantsFood);
        }

        // POST: TResturantsFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var resturantsFood = await _context.ResturantsFoods.FindAsync(id);
            _context.ResturantsFoods.Remove(resturantsFood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResturantsFoodExists(Guid id)
        {
            return _context.ResturantsFoods.Any(e => e.Id == id);
        }
    }
}
