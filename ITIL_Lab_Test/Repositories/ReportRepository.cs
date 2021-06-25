using ITIL_Lab_Test.Models;
using Microsoft.EntityFrameworkCore;
using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext appDbContext;

        public ReportRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<ReportViewModel> GetReportByOrderId(long id)
        {
            try
            {
                var data = await appDbContext.Orders.Where(e => e.Id == id)
                .Include(e => e.Supplier)
                .Include(e => e.OrderDetails)
                .ThenInclude(e => e.Product)
                .FirstOrDefaultAsync();

                var reportData = new ReportViewModel
                {
                    RefId = data.RefId.ToString(),
                    ExpectedDate = data.ExpectedDate.ToShortDateString(),
                    PoDate = data.PoDate.ToShortDateString(),
                    PoNo = data.PoNo,
                    Remark = data.Remark,
                    Supplier = data.Supplier.Name,
                    Subtotal = data.OrderDetails.Count > 0?
                        data.OrderDetails.Select(e => e.Qty * e.Product.Rate).Sum().ToString("F"): "0.00",
                    Products = data.OrderDetails.Select(e => new OrderedProductsViewModel
                    {
                        Name = e.Product.Name,
                        Qty = e.Qty.ToString("F"),
                        Rate = e.Product.Rate.ToString("F"),
                        Total = (e.Qty * e.Product.Rate).ToString("F")
                    }).ToList()
                };
                return reportData;
            }
            catch(Exception e)
            {

            }
            return null;
        }
    }
}
