using static System.Net.Mime.MediaTypeNames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFlowers.Models
{
    public class Bouqet
    {
        [Key]
        public int BouqetId { get; set; }
        public List<MonoFlowers> Flowers { get; set; } = new List<MonoFlowers>();

        [NotMapped] 
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
