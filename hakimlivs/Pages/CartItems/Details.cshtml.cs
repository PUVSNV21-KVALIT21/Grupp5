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
    public class DetailsModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public DetailsModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public CartItem CartItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CartItem = await _context.CartItems.FirstOrDefaultAsync(m => m.Id == id);

            if (CartItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
