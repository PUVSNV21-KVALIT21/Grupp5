using System;
using System.Collections.Generic;

namespace hakimlivs.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime PrintDate { get; set; }
    }
}
