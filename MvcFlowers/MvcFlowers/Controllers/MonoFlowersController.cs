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
    public class MonoFlowersController : Controller
    {
        private readonly MvcFlowersContext _context;

        public MonoFlowersController(MvcFlowersContext context)
        {
            _context = context;
        }

        // GET: MonoFlowers
        public async Task<IActionResult> Index()
        {
            return View(await _context.MonoFlowers.ToListAsync());
        }

        // GET: MonoFlowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monoFlowers = await _context.MonoFlowers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monoFlowers == null)
            {
                return NotFound();
            }

            return View(monoFlowers);
        }

        // GET: MonoFlowers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MonoFlowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RecievementDate,Price,Colour")] MonoFlowers monoFlowers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monoFlowers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monoFlowers);
        }

        // GET: MonoFlowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monoFlowers = await _context.MonoFlowers.FindAsync(id);
            if (monoFlowers == null)
            {
                return NotFound();
            }
            return View(monoFlowers);
        }

        // POST: MonoFlowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RecievementDate,Price,Colour")] MonoFlowers monoFlowers)
        {
            if (id != monoFlowers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monoFlowers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonoFlowersExists(monoFlowers.Id))
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
            return View(monoFlowers);
        }

        // GET: MonoFlowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monoFlowers = await _context.MonoFlowers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monoFlowers == null)
            {
                return NotFound();
            }

            return View(monoFlowers);
        }

        // POST: MonoFlowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monoFlowers = await _context.MonoFlowers.FindAsync(id);
            if (monoFlowers != null)
            {
                _context.MonoFlowers.Remove(monoFlowers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonoFlowersExists(int id)
        {
            return _context.MonoFlowers.Any(e => e.Id == id);
        }
    }
}
