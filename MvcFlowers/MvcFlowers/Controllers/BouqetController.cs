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
            return View(await _context.Bouqet.Include(b => b.Flowers).ToListAsync());
        }

        // GET: Bouqet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bouqet = await _context.Bouqet
                .Include(b => b.Flowers) // Включаем цветы в детали букета
                .FirstOrDefaultAsync(m => m.BouqetId == id);
            if (bouqet == null)
            {
                return NotFound();
            }

            return View(bouqet);
        }

        // GET: Bouqet/Create
        public IActionResult Create()
        {
            var bouqet = new Bouqet
            {
                Flowers = new List<MonoFlowers>(), // Инициализация списка цветов
                SelectedFlowerIds = new List<int>()
            };

            // Получаем доступные цветы из базы данных
            var availableFlowers = _context.MonoFlowers.ToList(); // Предполагается, что у вас есть контекст базы данных
            
            // Проверяем, есть ли доступные цветы
            if (availableFlowers == null || !availableFlowers.Any())
            {
                ViewBag.AvailableFlowers = new SelectList(new List<MonoFlowers>(), "MonoFlowerId", "DisplayName");
            }
            else
            {
                ViewBag.AvailableFlowers = new SelectList(availableFlowers, "MonoFlowerId", "DisplayName");
            }


            return View(bouqet);
        }




        // POST: Bouqet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BouqetId, SelectedFlowerIds")] Bouqet bouqet)
        {
            try
            {
                // Проверяем, что SelectedFlowerIds не null
                if (bouqet.SelectedFlowerIds == null)
                {
                    bouqet.SelectedFlowerIds = new List<int>();
                }

                // Инициализируем Flowers, если это необходимо
                bouqet.Flowers ??= new List<MonoFlowers>(); // Используем оператор null-объединения

                if (ModelState.IsValid)
                {
                    // Получаем выбранные цветы из базы данных
                    bouqet.Flowers = await _context.MonoFlowers
                        .Where(f => bouqet.SelectedFlowerIds.Contains(f.MonoFlowerId))
                        .ToListAsync();

                    // Проверяем, были ли найдены цветы
                    if (bouqet.Flowers == null || !bouqet.Flowers.Any())
                    {
                        ModelState.AddModelError(string.Empty, "Не найдено ни одного выбранного цвета.");
                        return View(bouqet);
                    }

                    // Добавляем букет в контекст
                    _context.Bouqet.Add(bouqet);

                    // Сохраняем изменения
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(bouqet);
            }
            catch (Exception ex)
            {
                // Логируем информацию об ошибке
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Вы можете также добавить сообщение об ошибке в ModelState, чтобы отобразить его в представлении
                ModelState.AddModelError(string.Empty, "Произошла ошибка при создании букета. Пожалуйста, попробуйте еще раз.");
                return View(bouqet); // Возвращаем представление с текущими данными
            }
        }





        // GET: Bouqet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bouqet = await _context.Bouqet
                .Include(b => b.Flowers) // Убедитесь, что цветы загружаются
                .FirstOrDefaultAsync(m => m.BouqetId == id);
            if (bouqet == null)
            {
                return NotFound();
            }

            // Если Flowers равен null, инициализируйте его
            if (bouqet.Flowers == null)
            {
                bouqet.Flowers = new List<MonoFlowers>();
            }

            return View(bouqet);
        }

        // POST: Bouqet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BouqetId, Flowers")] Bouqet bouqet)
        {
            if (id != bouqet.BouqetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Убедитесь, что цветы не равны null
                    if (bouqet.Flowers == null)
                    {
                        bouqet.Flowers = new List<MonoFlowers>(); // Инициализация списка, если он null
                    }

                    _context.Update(bouqet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BouqetExists(bouqet.BouqetId))
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
                .Include(b => b.Flowers) // Включаем цветы в детали букета
                .FirstOrDefaultAsync(m => m.BouqetId == id);
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
            return _context.Bouqet.Any(e => e.BouqetId == id);
        }

    }
}
