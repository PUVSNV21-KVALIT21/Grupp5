using hakimlivs.Models;

namespace hakimlivs.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext database)
        {
            if (database.Orders.Any())
            {
                return;
            }

            string[] lines = File.ReadAllLines("Products.csv");

            foreach (string line in lines)
            {
                string[] values = line.Split(";");

                Product product = new Product
                {
                    Name = values[0],
                    Price = decimal.Parse(values[1]),
                    Category = values[2],
                    Info = values[3],
                    Image = values[4],
                };
                database.Products.Add(product);
            }

            User user = new User
            {
                FirstName = "Brad",
                LastName = "Pitt",
                IsAdmin = false,
                Email = "Brad.pitt@hollywood.com"
            };
            database.Users.Add(user);

            Order order = new Order
            {
                User = user,
                Date = DateTime.Now
            };
            database.Orders.Add(order);
            database.SaveChanges();
        }
    }
}
