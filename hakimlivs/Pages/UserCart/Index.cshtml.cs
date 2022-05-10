using hakimlivs.Data;
using hakimlivs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hakimlivs.Pages.UserCart
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;
        public IndexModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        [BindProperty]
        public decimal Sum { get; set; }
        public Cart Cart { get; set; }
        public List<CartItem> CartItems { get; set; }
        public Order Order { get; set; }
        public OrderDetails OrderDetails { get; set; }

        private decimal CheckPrice(List<CartItem> cartItems)
        {
            foreach (var product in cartItems)
            {
                Sum += product.Amount * product.Product.Price;
            }

            return Sum;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (accessControl.LoggedInUserID == null)
            {
                return Forbid();
            }

            Cart = await database.Carts.Where(c => c.UserId == accessControl.LoggedInUserID).FirstOrDefaultAsync();
            CartItems = await database.CartItems.Where(c => c.Cart == Cart).Include(c => c.Product).ToListAsync();

            CheckPrice(CartItems);

            return Page();
        }
        public async Task OnPostAsync()
        {
            Order = new Order
            {
                UserId = accessControl.LoggedInUserID
            };

            Cart = await database.Carts.Where(c => c.UserId == accessControl.LoggedInUserID).FirstOrDefaultAsync();
            CartItems = await database.CartItems.Where(c => c.Cart == Cart).Include(c => c.Product).ToListAsync();

            List<OrderDetails> details = new List<OrderDetails>();

            foreach (var product in CartItems)
            {
                OrderDetails = new OrderDetails
                {
                    Product = product.Product,
                    Amount = product.Amount,
                };
                details.Add(OrderDetails);
                database.OrderDetails.Add(OrderDetails);
            }

            Order.OrderDetails = details;

            CheckPrice(CartItems);
            
            database.Orders.Add(Order);
            database.Carts.Remove(Cart);
            database.CartItems.RemoveRange(CartItems);

            database.SaveChanges();
            
        }
        public async Task<IActionResult> OnPostPlus(int id)
        {
            var cartItem = await database.CartItems.FindAsync(id);

            cartItem.Amount++;


            await database.SaveChangesAsync();
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostMinus(int id)
        {
            var cartItem = await database.CartItems.FindAsync(id);

            if (cartItem.Amount <= 1)
            {
                database.CartItems.Remove(cartItem);
                await database.SaveChangesAsync();
            }
            else
            {
                cartItem.Amount--;
                await database.SaveChangesAsync();
            }


            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var cartItem = await database.CartItems.FindAsync(id);


            database.CartItems.Remove(cartItem);
            await database.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
