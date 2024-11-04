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
    public class BouqetController : Controller
    {
        private readonly MvcFlowersContext _context;

        public BouqetController(MvcFlowersContext context)
        {
            _context = context;
        }

        // GET: Bouqet
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bouqet.ToListAsync());
        }

        // GET: Bouqet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bouqet = await _context.Bouqet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bouqet == null)
            {
                return NotFound();
            }

            return View(bouqet);
        }

        // GET: Bouqet/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bouqet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Bouqet bouqet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bouqet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bouqet);
        }

        // GET: Bouqet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bouqet = await _context.Bouqet.FindAsync(id);
            if (bouqet == null)
            {
                return NotFound();
            }
            return View(bouqet);
        }

        // POST: Bouqet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Bouqet bouqet)
        {
            if (id != bouqet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bouqet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BouqetExists(bouqet.Id))
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
            return View(bouqet);
        }

        // GET: Bouqet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bouqet = await _context.Bouqet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bouqet == null)
            {
                return NotFound();
            }

            return View(bouqet);
        }

        // POST: Bouqet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bouqet = await _context.Bouqet.FindAsync(id);
            if (bouqet != null)
            {
                _context.Bouqet.Remove(bouqet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BouqetExists(int id)
        {
            return _context.Bouqet.Any(e => e.Id == id);
        }
    }
}
