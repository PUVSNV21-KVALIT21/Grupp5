using hakimlivs.Data;
using hakimlivs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hakimlivs.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel(ApplicationDbContext database, AccessControl accessControl, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.database = database;
            this.accessControl = accessControl;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        public List<Order> Order { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (accessControl.LoggedInUserID == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {
                Order = await database.Orders.OrderBy(o => o.Date).Include(o => o.User).ToListAsync();
            }
            else
            {
                Order = await database.Orders.Where(o => o.UserId == accessControl.LoggedInUserID).ToListAsync();
            }
            
            return Page();

        }
    }
}
