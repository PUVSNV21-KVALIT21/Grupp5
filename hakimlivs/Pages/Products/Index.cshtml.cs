using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [FromQuery]
        public string Filter { get; set; }
        [FromForm]
        public string SearchTerm { get; set; }
        public Category Category { get; set; }
        
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

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.Where(c => c.Name == Filter).FirstOrDefaultAsync();

            if (Filter == null)
            {
                Products = await _context.Products.ToListAsync();
            }
            else
            {
                Products = await _context.Products.Where(p => p.Category == Category).ToListAsync();
            }
        }

        public async Task OnPostAsync()
        {
            if (SearchTerm == null)
            {
                Products = await _context.Products.ToListAsync();
            }
            else
            {
                Products = await _context.Products.Where(p => p.Name.Contains(SearchTerm) || p.Info.Contains(SearchTerm)).ToListAsync();
            }
    }
}
