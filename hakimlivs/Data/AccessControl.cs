using hakimlivs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace hakimlivs.Data
{
    public class AccessControl
    {
        public string LoggedInUserID { get; private set; }

        public AccessControl(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            LoggedInUserID = userManager.GetUserId(httpContextAccessor.HttpContext.User);
        }


    }
}
