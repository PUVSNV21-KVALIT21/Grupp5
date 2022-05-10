using hakimlivs.Data;
using hakimlivs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public List<OrderDetails> OrderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (accessControl.LoggedInUserID == null)
            {
                return Forbid();
            }

            var order = await database.Orders.FindAsync(id);
            var orderDetails = await database.OrderDetails.Where(o => o.Order == order).Include(o => o.Product).ToListAsync();
            OrderDetails = orderDetails;

            return Page();
        }
    }
}
