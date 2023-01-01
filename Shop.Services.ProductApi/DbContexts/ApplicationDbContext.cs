using Microsoft.EntityFrameworkCore;
using Shop.Services.ProductApi.Models;

namespace Shop.Services.ProductApi.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId= 1,
                Name = "Producr1",
                Price = 1,
                CategoryName="Desert",
                ImageUrl = "",
               Description = "Description1"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Producr21",
                Price = 2,
                CategoryName = "Desert",
                ImageUrl = "",
                Description = "Description2"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Producr3",
                Price = 3,
                CategoryName = "Desert3",
                ImageUrl = "",
                Description = "Description3"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Producr4",
                Price = 4,
                CategoryName = "Desert4",
                ImageUrl = "",
                Description = "Description4"
            });
        }
        public DbSet<Product> Products { get; set; }
    }
}
