using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int Amount { get; set; }

    }
}
