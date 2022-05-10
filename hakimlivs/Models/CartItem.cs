using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Ammount { get; set; }
        [Required]
        public Cart Cart { get; set; }

    }
}
