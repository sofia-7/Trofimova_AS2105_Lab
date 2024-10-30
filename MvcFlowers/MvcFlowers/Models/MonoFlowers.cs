using System.ComponentModel.DataAnnotations;
namespace MvcFlowers.Models
{
    public class MonoFlowers
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime RecievementDate { get; set; }
        public decimal Price { get; set; }
        public string? Colour { get; set; }
    }
}
