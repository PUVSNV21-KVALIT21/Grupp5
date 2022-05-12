using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;

namespace hakimlivs.Pages.Invoices
{
    public class DeleteModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public DeleteModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Invoice = await _context.Invoices.FirstOrDefaultAsync(m => m.Id == id);

            if (Invoice == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Invoice = await _context.Invoices.FindAsync(id);

            if (Invoice != null)
            {
                _context.Invoices.Remove(Invoice);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
