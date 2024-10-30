using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace MvcFlowers.Models
{
    public class PottedFlowers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime RecievementDate { get; set; }
        public decimal Price { get; set; }
        public float? Temperature { get; set; }
        public string? Lightning { get; set; }
    }

}

