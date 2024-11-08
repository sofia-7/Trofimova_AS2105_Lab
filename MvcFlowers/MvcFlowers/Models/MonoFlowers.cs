﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MvcFlowers.Models
{
    public class MonoFlowers
    {
        [Key]
        public int MonoFlowerId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime RecievementDate { get; set; }
        public decimal Price { get; set; }
        public string? Colour { get; set; }

        [NotMapped] 
        public int PriceAsInt
        {
            get => (int)Price; 
            set => Price = value; 
        }
        
        public string DisplayName => $"{Name} ({Colour})";
    
    // Метод для подсчета свежести цветка
    public int CalculateFreshness()
        {
            // Получаем количество дней, прошедших с момента получения цветка
            int daysSinceReceived = (DateTime.Now - RecievementDate).Days;
            return daysSinceReceived;
        }

        // Метод для проверки свежести цветка
        public bool IsFresh()
        {
            return CalculateFreshness() < 7; // Свежий, если меньше 7 дней
        }
    }
}
