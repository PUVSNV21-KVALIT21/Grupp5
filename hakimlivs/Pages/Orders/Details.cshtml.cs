using hakimlivs.Data;
using hakimlivs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace hakimlivs.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;
        public DetailsModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        
        public Invoice Invoice { get; set; }
        public List<Invoice> Invoices { get; set; }
        public Order Order { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public int Index { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (accessControl.LoggedInUserID == null)
            {
                return Forbid();
            }

            var order = await database.Orders.FindAsync(id);
            var orderDetails = await database.OrderDetails.Where(o => o.Order == order).Include(o => o.Product).ToListAsync();
            OrderDetails = orderDetails;
            Order = order;
            Invoices = await database.Invoices.ToListAsync();
            //kollar im en faktura redan finns med OrderId, i sånna fall ska inte knappen för att skriva ut den synas
            Index = Invoices.FindIndex(m => m.OrderId == Order.Id);
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {            
            Order = await database.Orders.FindAsync(id);

            Invoice = new Invoice
            {
                OrderId = Order.Id,
                UserId = Order.UserId,
                PrintDate = DateTime.Now
            };
            database.Invoices.Add(Invoice);
            database.SaveChanges();
            return RedirectToPage("/Invoices/Details", new { id = Invoice.Id });
        }

    }
}
