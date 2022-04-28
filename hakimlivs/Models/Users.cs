using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public class Users : IdentityUser
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string Phone { get; set; }
        [Required]

        public string StreetAdress { get; set; }
        [Required]

        public string PostalCode { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public bool IsAdmin { get; set; } = false;
    }
}
