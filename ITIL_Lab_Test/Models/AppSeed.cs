using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.Models
{
    public static class AppSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1", Rate = 102 },
                new Product { Id = 2, Name = "Product 2", Rate = 10 },
                new Product { Id = 3, Name = "Product 3", Rate = 122 },
                new Product { Id = 4, Name = "Product 4", Rate = 202 },
                new Product { Id = 5, Name = "Product 5", Rate = 62 },
                new Product { Id = 6, Name = "Product 6", Rate = 12 }
                );
            builder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Supplier 1", Mobile = "017783447753", Address = "Address 1" },
                new Supplier { Id = 2, Name = "Supplier 2", Mobile = "017783447751", Address = "Address 2" },
                new Supplier { Id = 3, Name = "Supplier 3", Mobile = "017783447754", Address = "Address 3" },
                new Supplier { Id = 4, Name = "Supplier 4", Mobile = "017783447755", Address = "Address 4" }
                );
        }
    }
}
