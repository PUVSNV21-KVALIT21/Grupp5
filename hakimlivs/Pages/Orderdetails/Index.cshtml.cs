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
    public class IndexModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public IndexModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderDetails> OrderDetails { get;set; }

        public async Task OnGetAsync()
        {
            OrderDetails = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product).ToListAsync();
        }
    }
}
