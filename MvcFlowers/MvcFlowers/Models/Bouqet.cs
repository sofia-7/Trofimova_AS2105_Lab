using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MvcFlowers.Data;
using Microsoft.EntityFrameworkCore;

namespace MvcFlowers.Models
{
    public class Bouqet
    {
        [Key]
        public int BouqetId { get; set; }
        public List<MonoFlowers> Flowers { get; set; } = new List<MonoFlowers>();

        //[NotMapped]
        public decimal TotalPrice { get; set; }
        [NotMapped]
        public int FlowersCount { get; set; }
        public string SelectedFlowerIds { get; set; }

        public Bouqet()
        {
            Flowers = new List<MonoFlowers>();
        }

        // Метод для подсчета стоимости букета
        public decimal CalculateTotalPrice()
        {
            return Flowers.Sum(flower => flower.Price);
        }

        // Метод для подсчета количества цветов в букете
        public int CountFlowers()
        {
            return Flowers.Count;
        }

        // Метод для создания букета
        public static async Task<Bouqet> CreateBouquet(MvcFlowersContext context, string selectedFlowerIds)
        {
            var bouquet = new Bouqet();

            // Преобразуем строку идентификаторов в список целых чисел
            var selectedFlowerIdList = selectedFlowerIds
                .Split(',')
                .Select(id => int.TryParse(id, out var parsedId) ? parsedId : (int?)null)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            // Получаем все MonoFlowerId, которые уже существуют в других букете
            var existingFlowerIds = await context.Bouqet
                .SelectMany(b => b.Flowers.Select(f => f.MonoFlowerId)) // Извлекаем MonoFlowerId
                .Distinct() // Убираем дубликаты
                .ToListAsync();

            // Извлекаем доступные цветы, которые не существуют в других букете
            bouquet.Flowers = await context.MonoFlowers
                .Where(f => selectedFlowerIdList.Contains(f.MonoFlowerId) && !existingFlowerIds.Contains(f.MonoFlowerId))
                .ToListAsync();

            if (!bouquet.Flowers.Any())
            {
                throw new InvalidOperationException("Не найдено ни одного выбранного цветка, который можно добавить.");
            }

            return bouquet;
        }

        public static async Task<Bouqet> EditBouquet(MvcFlowersContext context, Bouqet bouqet, List<int> selectedFlowerIds)
        {
            // Извлечение всех MonoFlowerId, которые уже существуют в других букете
            var existingFlowerIds = await context.Bouqet
                .Where(b => b.BouqetId != bouqet.BouqetId) // Исключаем текущий букет
                .SelectMany(b => b.Flowers.Select(f => f.MonoFlowerId)) // Извлекаем MonoFlowerId
                .Distinct() // Убираем дубликаты
                .ToListAsync();

            // Извлечение цветов из базы данных
            var selectedFlowers = await context.MonoFlowers
                .Where(f => selectedFlowerIds.Contains(f.MonoFlowerId))
                .ToListAsync();

            var existingBouquet = await context.Bouqet
                .Include(b => b.Flowers)
                .FirstOrDefaultAsync(b => b.BouqetId == bouqet.BouqetId);

            if (existingBouquet == null)
            {
                throw new InvalidOperationException("Букет не найден.");
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

            return existingBouquet;
        }

        public static async Task DeleteBouquet(MvcFlowersContext context, int bouqetId)
        {
            var bouqet = await context.Bouqet
                .Include(b => b.Flowers)
                .FirstOrDefaultAsync(b => b.BouqetId == bouqetId);

            if (bouqet == null)
            {
                throw new InvalidOperationException("Букет не найден.");
            }

            // Удаляем все цветы из букета
            bouqet.Flowers.Clear();

            // Удаляем букет из контекста
            context.Bouqet.Remove(bouqet);
        }

        public static async Task DeleteFlower(MvcFlowersContext context, int bouqetId, int flowerId)
        {
            var bouqet = await context.Bouqet
                .Include(b => b.Flowers)
                .FirstOrDefaultAsync(b => b.BouqetId == bouqetId);

            if (bouqet == null)
            {
                throw new InvalidOperationException("Букет не найден.");
            }

            var flowerToRemove = bouqet.Flowers.FirstOrDefault(f => f.MonoFlowerId == flowerId);
            if (flowerToRemove == null)
            {
                throw new InvalidOperationException("Цветок не найден в букете.");
            }

            bouqet.Flowers.Remove(flowerToRemove);
        }

        public static async Task<List<int>> GetExistingMonoFlowerIds(MvcFlowersContext context, int currentBouqetId)
        {
            // Получаем все MonoFlowerId, которые уже существуют в других букете
            var existingMonoFlowerIds = await context.Bouqet
                .Where(b => b.BouqetId != currentBouqetId) // Исключаем текущий букет
                .SelectMany(b => b.Flowers.Select(f => f.MonoFlowerId)) // Извлекаем MonoFlowerId
                .Distinct() // Убираем дубликаты
                .ToListAsync();

            return existingMonoFlowerIds;
        }

    }
}
