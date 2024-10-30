using System.ComponentModel.DataAnnotations;
namespace MvcFlowers.Models
{
    public class Bouqets
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationtDate { get; set; }
        public decimal Price { get; set; }
        public string Flower_type { get; set; }
        public string? Colour { get; set; }
    }
}
