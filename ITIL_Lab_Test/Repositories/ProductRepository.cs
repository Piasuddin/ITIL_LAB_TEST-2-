using ITIL_Lab_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ITIL_Lab_Test.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<List<Product>> GetAll()
        {
            return await appDbContext.Products.ToListAsync();
        }

        public async Task<List<Product>> SearchProduct(string input)
        {
            return await appDbContext.Products.Where(e => e.Name.Contains(input)).ToListAsync();
        }
    }
}
