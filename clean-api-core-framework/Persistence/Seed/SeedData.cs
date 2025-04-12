using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence.Seed
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Products.Any()) // Check if products already exist
                {
                    context.Products.AddRange(
                        new Product { Name = "Product 1", Description = "Dairy", Price = 100 },
                        new Product { Name = "Product 2", Description = "Fruits", Price = 150 },
                        new Product { Name = "Product 3", Description = "Meat", Price = 200 }
                    );

                    context.SaveChanges(); // Save changes to the database
                }
            }
        }
    }
}