using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using hakimlivs.Data;
using hakimlivs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace hakimlivs.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;
        private readonly AccessControl accessControl;

        public CreateModel(hakimlivs.Data.ApplicationDbContext context, AccessControl accessControl)
        {
            _context = context;
            this.accessControl = accessControl;
        }

        public IActionResult OnGet()
        {
            if (accessControl.LoggedInUserID == null)
            {
                return Forbid();
            }
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
