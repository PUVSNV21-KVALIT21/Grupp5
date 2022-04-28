using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class Orders
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
