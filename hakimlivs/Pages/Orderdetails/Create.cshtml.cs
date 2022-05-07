using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using hakimlivs.Data;
using hakimlivs.Models;

namespace hakimlivs.Pages.Orderdetails
{
    public class CreateModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public CreateModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "UserId");
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Category");
            return Page();
        }

        [BindProperty]
        public OrderDetails OrderDetails { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderDetails.Add(OrderDetails);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
