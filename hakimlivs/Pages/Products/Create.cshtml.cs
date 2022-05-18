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
        //private IHostingEnvironment _environment;

        public CreateModel(hakimlivs.Data.ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        //[BindProperty]
        //public IFormFile Upload { get; set; }
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
            Product = new Product();

            Category = await database.Categories.FindAsync(product.Category.ID);

            //string path = Path.Combine(_environment.WebRootPath, "Images");
            //string fileName = Path.GetFileName(Upload.FileName);

            //using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            //{
            //    Upload.CopyTo(stream);
            //}

            Product.Name = product.Name;
            Product.Price = product.Price;
            Product.Category = Category;
            Product.Info = product.Info;
            Product.Image = product.Image;

            await database.Products.AddAsync(Product);
            await database.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Product.Id });
        }

        //public async Task OnPostUpload()
        //{
            
        //}
    }
}
