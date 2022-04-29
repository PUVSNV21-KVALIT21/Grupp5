using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}