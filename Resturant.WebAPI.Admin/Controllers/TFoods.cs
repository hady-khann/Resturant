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
    public class TFoods : Controller
    {
        private readonly ResturantContext _context;

        public TFoods(ResturantContext context)
        {
            _context = context;
        }

        // GET: TFoods
        public async Task<IActionResult> Index()
        {
            var resturantContext = _context.Foods.Include(f => f.Type);
            return View(await resturantContext.ToListAsync());
        }

        // GET: TFoods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: TFoods/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.FoodTypes, "Id", "Type");
            return View();
        }

        // POST: TFoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeId,FoodName,Pic,Description")] Food food)
        {
            if (ModelState.IsValid)
            {
                food.Id = Guid.NewGuid();
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.FoodTypes, "Id", "Type", food.TypeId);
            return View(food);
        }

        // GET: TFoods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.FoodTypes, "Id", "Type", food.TypeId);
            return View(food);
        }

        // POST: TFoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TypeId,FoodName,Pic,Description")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
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
            ViewData["TypeId"] = new SelectList(_context.FoodTypes, "Id", "Type", food.TypeId);
            return View(food);
        }

        // GET: TFoods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: TFoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var food = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(Guid id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }
    }
}
