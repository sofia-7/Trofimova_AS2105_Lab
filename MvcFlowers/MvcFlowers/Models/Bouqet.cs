using static System.Net.Mime.MediaTypeNames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcFlowers.Models
{
    public class Bouqet
    {
        public int Id { get; set; }
        public List<MonoFlowers> Flowers { get; set; } = new List<MonoFlowers>();

        // Метод для подсчета стоимости букета
        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var flower in Flowers)
            {
                totalPrice += flower.Price;
            }

            return totalPrice;
        }

        // Метод для подсчета количества цветов в букете
        public int CountFlowers()
        {
            return Flowers.Count;
        }
    }


}
