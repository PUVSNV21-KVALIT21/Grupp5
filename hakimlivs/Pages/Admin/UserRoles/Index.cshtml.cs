using hakimlivs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hakimlivs.Pages.Admin.UserRoles
{
    [Authorize(Roles = "Admin, Basic")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [BindProperty]
        public List<UserRolesViewModel> UserRolesViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("Admin"))
            {
                var users = await _userManager.Users.ToListAsync();
                UserRolesViewModel = new List<UserRolesViewModel>();
                foreach (User user in users)
                {
                    var thisViewModel = new UserRolesViewModel();
                    thisViewModel.UserId = user.Id;
                    thisViewModel.Email = user.Email;
                    thisViewModel.FirstName = user.FirstName;
                    thisViewModel.LastName = user.LastName;
                    thisViewModel.Roles = await GetUserRoles(user);
                    UserRolesViewModel.Add(thisViewModel);
                }

            }
            else
            {
                var LoggedInUserID = _userManager.GetUserId(HttpContext.User);
                var users = await _userManager.Users.Where(u => u.Id == LoggedInUserID).ToListAsync();
                UserRolesViewModel = new List<UserRolesViewModel>();
                foreach (User user in users)
                {
                    var thisViewModel = new UserRolesViewModel();
                    thisViewModel.UserId = user.Id;
                    thisViewModel.Email = user.Email;
                    thisViewModel.FirstName = user.FirstName;
                    thisViewModel.LastName = user.LastName;
                    thisViewModel.Roles = await GetUserRoles(user);
                    UserRolesViewModel.Add(thisViewModel);
                }
            }
            return Page();
        }
        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}
