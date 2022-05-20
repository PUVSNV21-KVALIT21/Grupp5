using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public User User { get; set; }
        [Required]
        public string UserId { get; set; }
        public List<CartItem> CartItem {get; set;}

    }
}
