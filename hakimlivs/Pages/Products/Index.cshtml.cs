using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using hakimlivs.Data;
using hakimlivs.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hakimlivs.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly hakimlivs.Data.ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public IndexModel(hakimlivs.Data.ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public Product AddCartProduct { get; set; }
        public IList<Product> Product { get; set; }
        [FromForm]
        public int Id { get; set; }
        [FromQuery]
        public string Filter { get; set; }
        [FromForm]
        public string SearchTerm { get; set; }
        public Category Category { get; set; }
        public Cart Cart { get; set; }
        public CartItem CartItem { get; set; }
        public async Task OnGetAsync()
        {
            Category = await database.Categories.Where(c => c.Name == Filter).FirstOrDefaultAsync();

            if (Filter == null)
            {
                Product = await database.Products.ToListAsync();
            }
            else
            {
                Product = await database.Products.Where(p => p.Category == Category).ToListAsync();
            }
        }

        public async Task OnPostSearch()
        {
            if (SearchTerm == null)
            {
                Product = await database.Products.ToListAsync();
            }
            else
            {
                Product = await database.Products.Where(p => p.Name.Contains(SearchTerm) || p.Info.Contains(SearchTerm)).ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAdd(int id)
        {
            AddCartProduct = await database.Products.FindAsync(id);

            var checkCart = await database.Carts.Where(c => c.UserId == accessControl.LoggedInUserID).FirstOrDefaultAsync();

            if (checkCart == null)
            {
                Cart = new Cart
                {
                    UserId = accessControl.LoggedInUserID
                };

                CartItem = new CartItem
                {
                    Product = AddCartProduct,
                    Amount = 1,
                    Cart = Cart
                };

                List<CartItem> cartItems = new List<CartItem>();
                cartItems.Add(CartItem);

                Cart.CartItem = cartItems;

                database.CartItems.Add(CartItem);
                database.Carts.Add(Cart);
                database.SaveChanges();

            }
            else
            {
                var cartItemQuery = await database.CartItems.Where(c => c.Product == AddCartProduct).FirstOrDefaultAsync();

                if (cartItemQuery == null)
                {
                    CartItem = new CartItem
                    {
                        Product = AddCartProduct,
                        Amount = 1,
                        Cart = checkCart
                    };

                    List<CartItem> cartItems = new List<CartItem>();
                    cartItems.Add(CartItem);

                    checkCart.CartItem= cartItems;
                    database.CartItems.Add(CartItem);
                    database.SaveChanges();
                }
                else
                {
                    cartItemQuery.Amount += 1;
                    database.SaveChanges();
                }
            }
            return RedirectToPage();
        }
    }
}
