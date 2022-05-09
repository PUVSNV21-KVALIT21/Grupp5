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

        public IList<Product> Product { get;set; }
        [FromQuery]
        public string Filter { get; set; }

        [FromForm]
        public string SearchTerm { get; set; }
        public Category Category { get; set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.Where(c => c.Name == Filter).FirstOrDefaultAsync();

            if (Filter == null)
            {
                Product = await _context.Products.ToListAsync();
            }
            else
            {
                Product = await _context.Products.Where(p => p.Category == Category).ToListAsync();
            }
        }

        public async Task OnPostAsync()
        {
            if (SearchTerm == null)
            {
                Product = await _context.Products.ToListAsync();
            }
            else
            {
                Product = await _context.Products.Where(p => p.Name.Contains(SearchTerm) || p.Info.Contains(SearchTerm)).ToListAsync();
            }
        }
    }
}
