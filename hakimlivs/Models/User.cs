using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hakimlivs.Models
{
    public enum Roles
    {
        SuperAdmin,
        Admin,
        Moderator,
        Basic
    }
    public class User : IdentityUser
    {

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Street")]
        [StringLength(50, MinimumLength = 2)]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Street number")]
        [RegularExpression("([0-9]+)")]
        public int StreetNumber { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression("(^[0-9]{5})")]
        public int PostalCode { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(50, MinimumLength = 1)]
        public string City { get; set; }

        public List<Order> Orders { get; set; }
    }
}
