using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace eCommerce.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
       
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Achieve> CheckoutAchieve {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" },
                new Category { Id = 3, Name = "Clothing" },
                new Category { Id = 4, Name = "Home & Kitchen" },
                new Category { Id = 5, Name = "Toys" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Gaming Laptop", Price = 1200.00m, Image = "laptop.jpg", Quantity = 10, CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Description = "Latest Model", Price = 800.00m, Image = "smartphone.jpg", Quantity = 25, CategoryId = 1 },
                new Product { Id = 3, Name = "Bluetooth Headphones", Description = "Noise Cancelling", Price = 150.00m, Image = "headphones.jpg", Quantity = 40, CategoryId = 1 },
                new Product { Id = 4, Name = "Mystery Novel", Description = "Bestseller", Price = 15.99m, Image = "novel.jpg", Quantity = 50, CategoryId = 2 },
                new Product { Id = 5, Name = "Cookbook", Description = "Healthy Recipes", Price = 22.50m, Image = "cookbook.jpg", Quantity = 30, CategoryId = 2 },
                new Product { Id = 6, Name = "T-Shirt", Description = "100% Cotton", Price = 12.99m, Image = "tshirt.jpg", Quantity = 100, CategoryId = 3 },
                new Product { Id = 7, Name = "Jeans", Description = "Slim Fit", Price = 39.99m, Image = "jeans.jpg", Quantity = 60, CategoryId = 3 },
                new Product { Id = 8, Name = "Coffee Maker", Description = "12-cup", Price = 49.99m, Image = "coffeemaker.jpg", Quantity = 20, CategoryId = 4 },
                new Product { Id = 9, Name = "Blender", Description = "High Power", Price = 89.99m, Image = "blender.jpg", Quantity = 15, CategoryId = 4 },
                new Product { Id = 10, Name = "Action Figure", Description = "Superhero", Price = 19.99m, Image = "actionfigure.jpg", Quantity = 80, CategoryId = 5 },
                new Product { Id = 11, Name = "Board Game", Description = "Family Fun", Price = 29.99m, Image = "boardgame.jpg", Quantity = 35, CategoryId = 5 }
            );

            // Seed PaymentMethods
            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, Name = "Credit Card" },
                new PaymentMethod { Id = 2, Name = "PayPal" },
                new PaymentMethod { Id = 3, Name = "Bank Transfer" },
                new PaymentMethod { Id = 4, Name = "Cash on Delivery" }
            );


            // Seed Achieve
            modelBuilder.Entity<Achieve>().HasData(
                new Achieve { Id = 1, ProductId = 1, Quantity = 1, UserId = "user1", CreatedData = new DateTime(2025, 12, 4) },
                new Achieve { Id = 2, ProductId = 2, Quantity = 2, UserId = "user2", CreatedData = new DateTime(2025, 12, 4) },
                new Achieve { Id = 3, ProductId = 4, Quantity = 1, UserId = "user3", CreatedData = new DateTime(2025, 12, 4) },
                new Achieve { Id = 4, ProductId = 6, Quantity = 3, UserId = "user1", CreatedData = new DateTime(2025, 12, 4) },
                new Achieve { Id = 5, ProductId = 8, Quantity = 1, UserId = "user2", CreatedData = new DateTime(2025, 12, 4) }
                );


        }
    }
   
}
