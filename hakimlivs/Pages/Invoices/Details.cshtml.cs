using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;
using ceTe.DynamicPDF.Conversion;
using System.IO;

namespace hakimlivs.Pages.Invoices
{
    public class DetailsModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext _context;

        public DetailsModel(hakimlivs.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public User User { get; set; }
        public Order Order { get; set; }
        public Invoice Invoice { get; set; }
        public decimal TotalSum { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //hämtar allt vi behöver skriva ut
            Invoice = await _context.Invoices.FirstOrDefaultAsync(m => m.Id == id);
            User = await _context.Users.FirstOrDefaultAsync(m => m.Id == Invoice.UserId);
            Order = await _context.Orders.Include(m => m.OrderDetails).ThenInclude(m => m.Product).FirstOrDefaultAsync(m => m.Id == Invoice.OrderId);

            if (Invoice == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            string url = Request.Host.ToString();
            HtmlConversionOptions options = new HtmlConversionOptions(false);

            HtmlConverter htmlConverter = new HtmlConverter(new Uri(@"https://"+ url +"/Invoices/Details/" + id), options);
            string folderPath = @"C:\faktura\";
            Invoice = await _context.Invoices.FirstOrDefaultAsync(m => m.Id == id);
            User = await _context.Users.FirstOrDefaultAsync(m => m.Id == Invoice.UserId);

            if (Directory.Exists(folderPath))
            {
                htmlConverter.Convert(folderPath + User.Email + Invoice.PrintDate.ToString("yy/MM/dd") + ".pdf");
            }

            else
            {
                Directory.CreateDirectory(folderPath);
                htmlConverter.Convert(folderPath + User.Email + Invoice.PrintDate.ToString("yy/MM/dd") + ".pdf");
            }
            
            return RedirectToPage();
        }
    }
}
