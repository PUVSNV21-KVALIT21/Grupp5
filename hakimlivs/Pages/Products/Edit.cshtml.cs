using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;

namespace hakimlivs.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public EditModel(hakimlivs.Data.ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public Product Product { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public Category Category { get; set; }

        private async Task LoadProduct(int id)
        {
            Product = await database.Products.SingleAsync(p => p.Id == id);
        }
        public async Task OnGetAsync(int id)
        {
            await LoadProduct(id);

            Categories = await database.Categories.AsNoTracking()
                .Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

        }
        public async Task<IActionResult> OnPostAsync(int id, Product product)
        {
            await LoadProduct(id);

            Category = await database.Categories.FindAsync(product.Category.ID);

            Product.Name = product.Name;
            Product.Price = product.Price;
            Product.Category = Category;
            Product.Info = product.Info;
            Product.Image = product.Image;

            await database.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Product.Id });
        }
    }
}
