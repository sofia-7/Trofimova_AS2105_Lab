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
    public class PottedFlowersController : Controller
    {
        private readonly MvcFlowersContext _context;

        public PottedFlowersController(MvcFlowersContext context)
        {
            _context = context;
        }

        // GET: PottedFlowers
        public async Task<IActionResult> Index()
        {
            return View(await _context.PottedFlowers.ToListAsync());
        }

        // GET: PottedFlowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pottedFlowers = await _context.PottedFlowers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pottedFlowers == null)
            {
                return NotFound();
            }

            return View(pottedFlowers);
        }

        // GET: PottedFlowers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PottedFlowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RecievementDate,Price,Temperature,Lightning")] PottedFlowers pottedFlowers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pottedFlowers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pottedFlowers);
        }

        // GET: PottedFlowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pottedFlowers = await _context.PottedFlowers.FindAsync(id);
            if (pottedFlowers == null)
            {
                return NotFound();
            }
            return View(pottedFlowers);
        }

        // POST: PottedFlowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RecievementDate,Price,Temperature,Lightning")] PottedFlowers pottedFlowers)
        {
            if (id != pottedFlowers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pottedFlowers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PottedFlowersExists(pottedFlowers.Id))
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
            return View(pottedFlowers);
        }

        // GET: PottedFlowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pottedFlowers = await _context.PottedFlowers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pottedFlowers == null)
            {
                return NotFound();
            }

            return View(pottedFlowers);
        }

        // POST: PottedFlowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pottedFlowers = await _context.PottedFlowers.FindAsync(id);
            if (pottedFlowers != null)
            {
                _context.PottedFlowers.Remove(pottedFlowers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PottedFlowersExists(int id)
        {
            return _context.PottedFlowers.Any(e => e.Id == id);
        }
    }
}
