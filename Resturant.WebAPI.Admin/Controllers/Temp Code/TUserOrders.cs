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
    public class TUserOrders : Controller
    {
        private readonly ResturantContext _context;

        public TUserOrders(ResturantContext context)
        {
            _context = context;
        }

        // GET: TUserOrders
        public async Task<IActionResult> Index()
        {
            var resturantContext = _context.UserOrders.Include(u => u.Food).Include(u => u.Resturant).Include(u => u.User);
            return View(await resturantContext.ToListAsync());
        }

        // GET: TUserOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOrder = await _context.UserOrders
                .Include(u => u.Food)
                .Include(u => u.Resturant)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userOrder == null)
            {
                return NotFound();
            }

            return View(userOrder);
        }

        // GET: TUserOrders/Create
        public IActionResult Create()
        {
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description");
            ViewData["ResturantId"] = new SelectList(_context.Resturants, "Id", "Address");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: TUserOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ResturantId,FoodId,FoodCount")] UserOrder userOrder)
        {
            if (ModelState.IsValid)
            {
                userOrder.Id = Guid.NewGuid();
                _context.Add(userOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", userOrder.FoodId);
            ViewData["ResturantId"] = new SelectList(_context.Resturants, "Id", "Address", userOrder.ResturantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userOrder.UserId);
            return View(userOrder);
        }

        // GET: TUserOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOrder = await _context.UserOrders.FindAsync(id);
            if (userOrder == null)
            {
                return NotFound();
            }
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", userOrder.FoodId);
            ViewData["ResturantId"] = new SelectList(_context.Resturants, "Id", "Address", userOrder.ResturantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userOrder.UserId);
            return View(userOrder);
        }

        // POST: TUserOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,ResturantId,FoodId,FoodCount")] UserOrder userOrder)
        {
            if (id != userOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserOrderExists(userOrder.Id))
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
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", userOrder.FoodId);
            ViewData["ResturantId"] = new SelectList(_context.Resturants, "Id", "Address", userOrder.ResturantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userOrder.UserId);
            return View(userOrder);
        }

        // GET: TUserOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOrder = await _context.UserOrders
                .Include(u => u.Food)
                .Include(u => u.Resturant)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userOrder == null)
            {
                return NotFound();
            }

            return View(userOrder);
        }

        // POST: TUserOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userOrder = await _context.UserOrders.FindAsync(id);
            _context.UserOrders.Remove(userOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserOrderExists(Guid id)
        {
            return _context.UserOrders.Any(e => e.Id == id);
        }
    }
}
