using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;

namespace hakimlivs.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public IndexModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Orders
                .Include(o => o.User).ToListAsync();
        }
    }
}
