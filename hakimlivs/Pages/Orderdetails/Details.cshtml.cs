using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;

namespace hakimlivs.Pages.Orderdetails
{
    public class DetailsModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public DetailsModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderDetails OrderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderDetails = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product).FirstOrDefaultAsync(m => m.Id == id);

            if (OrderDetails == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
