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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace hakimlivs.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext database;
        private readonly AccessControl accessControl;
        private readonly IHostingEnvironment _environment;

        public CreateModel(hakimlivs.Data.ApplicationDbContext database, AccessControl accessControl, IHostingEnvironment environment)
        {
            this.database = database;
            this.accessControl = accessControl;
            this._environment = environment;
        }

        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public Product Product { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public Category Category { get; set; }
        
        public async Task OnGetAsync()
        {
            Categories = await database.Categories.AsNoTracking()
                .Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                })
                .ToListAsync();

        }
        public async Task<IActionResult> OnPostAsync(Product product)
        {
            string wwwPath = _environment.WebRootPath;
            string contentPath = _environment.ContentRootPath;

            string path = Path.Combine(_environment.WebRootPath, "Images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(Image.FileName);

            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                Image.CopyTo(stream);
            }


            Category = await database.Categories.FindAsync(product.Category.ID);

            Product = new Product();
            Product.Name = product.Name;
            Product.Price = product.Price;
            Product.Category = Category;
            Product.Info = product.Info;
            Product.Image = "/Images/" + fileName;

            await database.Products.AddAsync(Product);
            await database.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Product.Id });
        }
    }
}
