using ITIL_Lab_Test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext appDbContext;

        public SupplierRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<List<Supplier>> GetAll()
        {
            return await appDbContext.Suppliers.ToListAsync();
        }
    }
}
