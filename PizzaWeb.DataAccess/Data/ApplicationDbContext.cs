using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaWeb.DataAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderDetail> OrderDetails{ get; set; }
        public DbSet<Company> Companies{ get; set; }
        public DbSet<ShoppingCart> shoppingCarts{ get; set; }
        public DbSet<OrderHeader> OrderHeaders{ get; set; }
        public DbSet<ProductOrderDetail> ProductOrderDetails{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Veg Pizza", DisplayOrder = 1 },
                    new Category { Id = 2, Name = "Non-Veg Pizza", DisplayOrder = 2 },
                    new Category { Id = 3, Name = "Cold drinks", DisplayOrder = 3 }
                );


            modelBuilder.Entity<Company>().HasData(
                   new Company
                   {
                       Id = 1,
                       Name = "Tech Solution",
                       StreetAddress = "123 Tech St",
                       City = "Tech city",
                       PostalCode = "21312",
                       State = "IL",
                       PhoneNumber = "3003320001"
                   },

                     new Company
                     {
                         Id = 2,
                         Name = "Vivid Books",
                         StreetAddress = "124 vid St",
                         City = "Vid city",
                         PostalCode = "55533",
                         State = "IL",
                         PhoneNumber = "3003423456"
                     },

                     new Company
                     {
                         Id = 3,
                         Name = "Readers Club",
                         StreetAddress = "999 Main st",
                         City = "Vsc space city",
                         PostalCode = "99932",
                         State = "NY",
                         PhoneNumber = "1122887654"
                     }
               );


            modelBuilder.Entity<Product>().HasData(

              new Product
              {
                  Id = 1,
                  Title = "Onion pizza",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ListPrice = 99,
                  Price = 90,
                  Price50 = 85,
                  Price100 = 80,
                  CategoryId = 1,
                  ImageUrl = ""
              },
              new Product
              {
                  Id = 2,
                  Title = "Paneer Pizza",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ListPrice = 40,
                  Price = 30,
                  Price50 = 25,
                  Price100 = 20,
                  CategoryId = 1,
                  ImageUrl = ""
              },
              new Product
              {
                  Id = 3,
                  Title = "Capsicum Pizza",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ListPrice = 55,
                  Price = 50,
                  Price50 = 40,
                  Price100 = 35,
                  CategoryId = 1,
                  ImageUrl = ""
              },
              new Product
              {
                  Id = 4,
                  Title = "Classic Chicken Pizaa",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ListPrice = 70,
                  Price = 65,
                  Price50 = 60,
                  Price100 = 55,
                  CategoryId = 2,
                  ImageUrl = ""
              },
              new Product
              {
                  Id = 5,
                  Title = "Chicken Golden Pizza",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ListPrice = 30,
                  Price = 27,
                  Price50 = 25,
                  Price100 = 20,
                  CategoryId = 2,
                  ImageUrl = ""
              },
              new Product
              {
                  Id = 6,
                  Title = "Orange Pepsi",
                  Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                  ListPrice = 25,
                  Price = 23,
                  Price50 = 22,
                  Price100 = 20,
                  CategoryId = 3,
                  ImageUrl = ""
              }

              );
        }
    }
}
