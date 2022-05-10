using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;

namespace hakimlivs.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public IndexModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public IList<Product> Products { get;set; }
        [FromForm]
        public int Id { get; set; }
        public async Task OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var Product = await _context.Products.FindAsync(Id);

            CartItem cartItem = new CartItem
            {
                Product = Product,
                Ammount = 1
            };
            
            /*Cart cart = new Cart*/
            return RedirectToPage();
        }

    }
}
