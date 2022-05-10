using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        [Required]
        public Cart Cart { get; set; }

    }
}
