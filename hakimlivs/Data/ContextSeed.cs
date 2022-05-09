using hakimlivs.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;

namespace hakimlivs.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new User
            {
                UserName = "superadmin",
                Email = "superadmin@example.com",
                FirstName = "Admin",
                LastName = "Administrator",
                Street = "The Street",
                PostalCode = 12345,
                City = "The city",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }

            }
        }

        public static async Task InitializeUserAsync(ApplicationDbContext database, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (database.Products.Any())
            {
                return;
            }

            string[] userLines = File.ReadAllLines("Data/Users.csv");

            foreach (string line in userLines)
            {
                string[] parts = line.Split(';');

                string password = parts[8];
                string role = parts[9];

                User user = new User
                {
                    UserName = parts[0],
                    FirstName = parts[1],
                    LastName = parts[2],
                    Street = parts[3],
                    StreetNumber = int.Parse(parts[4]),
                    PostalCode = int.Parse(parts[5]),
                    City = parts[6],
                    Email = parts[7],
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await userManager.CreateAsync(user, password);
                if (role == "Admin")
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
                else
                {
                    await userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                }
            }
        }
        public static void InitializeCategories(ApplicationDbContext database)
        {
            if (database.Categories.Any())
            {
                return;
            }

            string[] categoryLines = File.ReadAllLines("Data/Categories.csv");

            foreach (string line in categoryLines)
            {
                Category category = new Category
                {
                    Name = line,
                };
                database.Categories.Add(category);
            }
            database.SaveChanges();
        } 

        public static void InitializeProducts(ApplicationDbContext database)
        {
            if (database.Products.Any())
            {
                return;
            }

            string[] lines = File.ReadAllLines("Products.csv");
            
            List<Category> categories = database.Categories.ToList();

            foreach (string line in lines)
            {
                string[] values = line.Split(";");

                Product product = new Product
                {
                    Name = values[0],
                    Price = decimal.Parse(values[1]),
                    Category = categories[int.Parse(values[2])-1],
                    Info = values[3],
                    Image = values[4],
                };
                database.Products.Add(product);
            }
            database.SaveChanges();
        }
    }
}
