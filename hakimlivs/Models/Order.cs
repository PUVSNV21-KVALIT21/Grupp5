using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class Order
    {
        public int Id { get; set; }

        public User User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}