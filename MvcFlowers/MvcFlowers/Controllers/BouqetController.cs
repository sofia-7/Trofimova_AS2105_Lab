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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: Bouqet
        public async Task<IActionResult> Index()
        {
            var bouquets = await _context.Bouqet.Include(b => b.Flowers).ToListAsync();

            // Вычисляем стоимость и сумму цветов для каждого букета
            foreach (var bouqet in bouquets)
            {
                bouqet.TotalPrice = bouqet.CalculateTotalPrice();
                bouqet.FlowersCount = bouqet.CountFlowers();
            }

            return View(bouquets);
        }

        // GET: Bouqet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Загружаем букет вместе с его цветами
            var bouqet = await _context.Bouqet
                .Include(b => b.Flowers) // Включаем цветы
                .FirstOrDefaultAsync(m => m.BouqetId == id);

            if (bouqet == null)
            {
                return NotFound();
            }

            // Устанавливаем количество цветов и общую стоимость
            bouqet.FlowersCount = bouqet.Flowers.Count;
            bouqet.TotalPrice = bouqet.Flowers.Sum(f => f.Price); 

            return View(bouqet);
        }


        // GET: Bouqet/Create
        public async Task<IActionResult> Create()
        {
            var bouqet = new Bouqet
            {
                Flowers = new List<MonoFlowers>(), // Инициализация списка цветов
            };

            // Получаем доступные цветы из базы данных
            var availableFlowers = _context.MonoFlowers.ToList();
            ViewBag.AvailableFlowers = new SelectList(availableFlowers, "MonoFlowerId", "DisplayName");
            // Получаем все MonoFlowerId, которые уже существуют в других букете
            var existingFlowerIds = await _context.Bouqet
                .SelectMany(b => b.Flowers.Select(f => f.MonoFlowerId)) // Извлекаем MonoFlowerId
                .Distinct() // Убираем дубликаты
                .ToListAsync();

            // Передаем в ViewBag для отображения в представлении
            ViewBag.ExistingFlowerIds = string.Join(", ", existingFlowerIds);
            return View(bouqet);
        }

        // POST: Bouqet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BouqetId, SelectedFlowerIds")] Bouqet bouqet)
        {
            if (ModelState.IsValid)
            {
                // Преобразуем строку идентификаторов в список целых чисел
                var selectedFlowerIds = bouqet.SelectedFlowerIds
                    .Split(',')
                    .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                    .Where(id => id.HasValue)
                    .Select(id => id.Value)
                    .ToList();

                // Получаем все MonoFlowerId, которые уже существуют в других букете
                var existingFlowerIds = await _context.Bouqet
                    .SelectMany(b => b.Flowers.Select(f => f.MonoFlowerId)) // Извлекаем MonoFlowerId
                    .Distinct() // Убираем дубликаты
                    .ToListAsync();


                // Извлекаем доступные цветы, которые не существуют в других букете
                bouqet.Flowers = await _context.MonoFlowers
                    .Where(f => selectedFlowerIds.Contains(f.MonoFlowerId) && !existingFlowerIds.Contains(f.MonoFlowerId))
                    .ToListAsync();

                if (!bouqet.Flowers.Any())
                {
                    ModelState.AddModelError(string.Empty, "Не найдено ни одного выбранного цветка, который можно добавить.");
                    return View(bouqet);
                }

                // Добавляем букет в контекст и сохраняем изменения
                _context.Bouqet.Add(bouqet);
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

            var bouqet = await _context.Bouqet
                .Include(b => b.Flowers)
                .FirstOrDefaultAsync(m => m.BouqetId == id);
            if (bouqet == null)
            {
                return NotFound();
            }

            // Получаем существующие MonoFlowerId в других букете
            var existingFlowerIds = await GetExistingMonoFlowerIds(bouqet.BouqetId);

            // Дополнительная логика, если необходимо

            bouqet.SelectedFlowerIds = string.Join(",", bouqet.Flowers.Select(f => f.MonoFlowerId));

            var availableFlowers = await _context.MonoFlowers.ToListAsync();
            ViewBag.AvailableFlowers = new SelectList(availableFlowers, "MonoFlowerId", "DisplayName");

            return View(bouqet);
        }

        // POST: Bouqet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BouqetId, SelectedFlowerIds")] Bouqet bouqet)
        {
            if (id != bouqet.BouqetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var selectedFlowerIds = bouqet.SelectedFlowerIds
                        .Split(',')
                        .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();

                    // Извлечение всех MonoFlowerId, которые уже существуют в других букете
                    var existingFlowerIds = await _context.Bouqet
                        .Where(b => b.BouqetId != bouqet.BouqetId) // Исключаем текущий букет
                        .SelectMany(b => b.Flowers.Select(f => f.MonoFlowerId)) // Извлекаем MonoFlowerId
                        .Distinct() // Убираем дубликаты
                        .ToListAsync();

                    // Извлечение цветов из базы данных
                    var selectedFlowers = await _context.MonoFlowers
                        .Where(f => selectedFlowerIds.Contains(f.MonoFlowerId))
                        .ToListAsync();

                    var existingBouquet = await _context.Bouqet
                        .Include(b => b.Flowers)
                        .FirstOrDefaultAsync(b => b.BouqetId == bouqet.BouqetId);

                    if (existingBouquet == null)
                    {
                        return NotFound();
                    }

                    // Добавление только новых цветов, которые не существуют в других букете
                    foreach (var flower in selectedFlowers)
                    {
                        // Добавляем только если цветок не существует в других букете
                        if (!existingBouquet.Flowers.Any(f => f.MonoFlowerId == flower.MonoFlowerId) &&
                            !existingFlowerIds.Contains(flower.MonoFlowerId))
                        {
                            existingBouquet.Flowers.Add(flower);
                        }
                    }

                    // Обновление букета
                    _context.Update(existingBouquet);
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



        // POST: Bouqet/DeleteFlower/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFlower(int bouqetId, int flowerId)
        {
            var bouqet = await _context.Bouqet.Include(b => b.Flowers)
                .FirstOrDefaultAsync(b => b.BouqetId == bouqetId);

            if (bouqet == null)
            {
                return NotFound();
            }

            // Удаляем цветок из букета
            var flowerToRemove = bouqet.Flowers.FirstOrDefault(f => f.MonoFlowerId == flowerId);
            if (flowerToRemove != null)
            {
                bouqet.Flowers.Remove(flowerToRemove);
            }

            // Обновляем SelectedFlowerIds
            bouqet.SelectedFlowerIds = string.Join(",", bouqet.Flowers.Select(f => f.MonoFlowerId));

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id = bouqetId });
        }

        // GET: Bouqet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Загружаем букет и его цветы
            var bouqet = await _context.Bouqet
                .Include(b => b.Flowers) // Включаем цветы
                .FirstOrDefaultAsync(m => m.BouqetId == id);

            if (bouqet == null)
            {
                return NotFound();
            }

            // Устанавливаем количество цветов и общую стоимость
            bouqet.FlowersCount = bouqet.Flowers.Count;
            bouqet.TotalPrice = bouqet.Flowers.Sum(f => f.Price); // Предполагаем, что у каждого цветка есть свойство Price

            return View(bouqet);
        }


        // POST: Bouqet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bouqet = await _context.Bouqet.Include(b => b.Flowers).FirstOrDefaultAsync(b => b.BouqetId == id);
            if (bouqet == null)
            {
                return NotFound();
            }

            // Удаляем все цветы из букета
            bouqet.Flowers.Clear();

            // Обновляем SelectedFlowerIds
            bouqet.SelectedFlowerIds = string.Empty; // Очищаем выбранные идентификаторы

            // Удаляем букет из контекста
            _context.Bouqet.Remove(bouqet);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BouqetExists(int id)
        {
            return _context.Bouqet.Any(e => e.BouqetId == id);
        }

        public async Task<IActionResult> GetExistingMonoFlowerIds(int currentBouqetId)
        {
            // Получаем все MonoFlowerId, которые уже существуют в других букетах
            var existingMonoFlowerIds = await _context.Bouqet
                .Where(b => b.BouqetId != currentBouqetId) // Исключаем текущий букет
                .SelectMany(b => b.Flowers.Select(f => f.MonoFlowerId)) // Извлекаем MonoFlowerId
                .Distinct() // Убираем дубликаты
                .ToListAsync();

            return Ok(existingMonoFlowerIds);
        }


    }
}
