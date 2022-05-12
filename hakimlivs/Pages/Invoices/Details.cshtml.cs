using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;

namespace hakimlivs.Pages.Invoices
{
    public class DetailsModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public DetailsModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public User User { get; set; }
        public Order Order { get; set; }
        public Invoice Invoice { get; set; }
        public decimal TotalSum { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //hämtar allt vi behöver skriva ut
            Invoice = await _context.Invoices.FirstOrDefaultAsync(m => m.Id == id);
            User = await _context.Users.FirstOrDefaultAsync(m => m.Id == Invoice.UserId);
            Order = await _context.Orders.Include(m => m.OrderDetails).ThenInclude(m => m.Product).FirstOrDefaultAsync(m => m.Id == Invoice.OrderId);

            if (Invoice == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
