using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcFlowers.Data;
using MvcFlowers.Models;

namespace MvcFlowers.Controllers
{
    public class BouqetsController : Controller
    {
        private readonly MvcFlowersContext _context;

        public BouqetsController(MvcFlowersContext context)
        {
            _context = context;
        }

        // GET: Bouqets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bouqets.ToListAsync());
        }

        // GET: Bouqets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bouqets = await _context.Bouqets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bouqets == null)
            {
                return NotFound();
            }

            return View(bouqets);
        }

        // GET: Bouqets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bouqets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,CreationtDate,Price,Flower_type, Colour")] Bouqets bouqets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bouqets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bouqets);
        }

        // GET: Bouqets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bouqets = await _context.Bouqets.FindAsync(id);
            if (bouqets == null)
            {
                return NotFound();
            }
            return View(bouqets);
        }

        // POST: Bouqets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,CreationtDate,Price,Flower_type, Colour")] Bouqets bouqets)
        {
            if (id != bouqets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bouqets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BouqetsExists(bouqets.Id))
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
            return View(bouqets);
        }

        // GET: Bouqets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bouqets = await _context.Bouqets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bouqets == null)
            {
                return NotFound();
            }

            return View(bouqets);
        }

        // POST: Bouqets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bouqets = await _context.Bouqets.FindAsync(id);
            if (bouqets != null)
            {
                _context.Bouqets.Remove(bouqets);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BouqetsExists(int id)
        {
            return _context.Bouqets.Any(e => e.Id == id);
        }
    }
}
