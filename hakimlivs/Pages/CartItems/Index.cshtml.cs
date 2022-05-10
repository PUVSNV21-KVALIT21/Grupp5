using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;

namespace hakimlivs.Pages.CartItems
{
    public class IndexModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public IndexModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CartItem> CartItem { get;set; }

        public async Task OnGetAsync()
        {
            CartItem = await _context.CartItems.ToListAsync();
        }
    }
}
