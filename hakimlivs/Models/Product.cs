using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Info { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
