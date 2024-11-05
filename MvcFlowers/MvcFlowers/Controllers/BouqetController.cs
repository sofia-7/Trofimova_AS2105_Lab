﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                //SelectedFlowerIds = new List<int>()
            };
            Console.WriteLine("SelectedFlowerIds: " + string.Join(",", bouqet.SelectedFlowerIds));
            // Получаем доступные цветы из базы данных
            var availableFlowers = _context.MonoFlowers.ToList(); 
            
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



        // Post: Bouqet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BouqetId, SelectedFlowerIds")] Bouqet bouqet)
        {
            try
            {
                // Инициализация SelectedFlowerIds как пустой строки, если она null
                if (string.IsNullOrEmpty(bouqet.SelectedFlowerIds))
                {
                    bouqet.SelectedFlowerIds = string.Empty; // Инициализируем как пустую строку, если нет выбранных цветов
                }

                // Преобразуем строку идентификаторов в список целых чисел
                var selectedFlowerIds = bouqet.SelectedFlowerIds
                    .Split(',')
                    .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                    .Where(id => id.HasValue)
                    .Select(id => id.Value)
                    .ToList();

                bouqet.Flowers ??= new List<MonoFlowers>();

                if (ModelState.IsValid)
                {
                    // Получаем выбранные цветы из базы данных
                    bouqet.Flowers = await _context.MonoFlowers
                        .Where(f => selectedFlowerIds.Contains(f.MonoFlowerId))
                        .ToListAsync();

                    if (bouqet.Flowers == null || !bouqet.Flowers.Any())
                    {
                        ModelState.AddModelError(string.Empty, "Не найдено ни одного выбранного цветка.");
                        return View(bouqet);
                    }

                    // Преобразуем список идентификаторов обратно в строку для хранения
                    bouqet.SelectedFlowerIds = string.Join(",", selectedFlowerIds);

                    // Добавляем букет в контекст и сохраняем изменения
                    _context.Bouqet.Add(bouqet);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(bouqet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                ModelState.AddModelError(string.Empty, "Произошла ошибка при создании букета. Пожалуйста, попробуйте еще раз.");
                return View(bouqet);
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

            // Инициализация SelectedFlowerIds, если она null
            bouqet.SelectedFlowerIds = string.Join(",", bouqet.Flowers.Select(f => f.MonoFlowerId));

            // Получаем доступные цветы из базы данных
            var availableFlowers = await _context.MonoFlowers.ToListAsync();

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
                    // Преобразуем строку идентификаторов в список целых чисел
                    var selectedFlowerIds = bouqet.SelectedFlowerIds
                        .Split(',')
                        .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();

                    // Получаем выбранные цветы из базы данных
                    bouqet.Flowers = await _context.MonoFlowers
                        .Where(f => selectedFlowerIds.Contains(f.MonoFlowerId))
                        .ToListAsync();

                    if (bouqet.Flowers == null || !bouqet.Flowers.Any())
                    {
                        ModelState.AddModelError(string.Empty, "Не найдено ни одного выбранного цветка.");
                        return View(bouqet);
                    }

                    // Обновляем букеты в контексте
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
