using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class Products
    {
        public enum Categories
        {
            Frukt,
            Soppa,
            Dricka,
            Godis
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Info { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
