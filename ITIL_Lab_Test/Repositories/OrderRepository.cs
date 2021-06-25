using ITIL_Lab_Test.Models;
using ITIL_Lab_Test.ViewModels;
using Microsoft.EntityFrameworkCore;
using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace ITIL_Lab_Test.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Order> Add(OrderCreateViewModel model)
        {

            Order order = null;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    order = new Order
                    {
                        ExpectedDate = model.ExpectedDate.Value,
                        PoDate = model.PoDate.Value,
                        PoNo = model.PoNo,
                        RefId = model.RefId,
                        Remark = model.Remark,
                        SupplierId = model.SupplierId.Value
                    };
                    await appDbContext.Orders.AddAsync(order);
                    await appDbContext.SaveChangesAsync();
                    List<OrderDetails> orderDetails = new List<OrderDetails>();
                    foreach (var item in model.OrderDetails)
                    {
                        var details = new OrderDetails
                        {
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            Qty = item.Qty
                        };
                        orderDetails.Add(details);
                    }
                    await appDbContext.AddRangeAsync(orderDetails);
                    await appDbContext.SaveChangesAsync();
                    transaction.Complete();
                }
            }
            catch (Exception e)
            {

            }
            return order;
        }
        public async Task<Order> Update(OrderCreateViewModel model)
        {

            Order order = null;
            try
            {
                order = await appDbContext.Orders.Where(e => e.Id == model.Id)
                    .Include(e => e.OrderDetails).FirstOrDefaultAsync();
                if (order != null)
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        order.ExpectedDate = model.ExpectedDate.Value;
                        order.PoDate = model.PoDate.Value;
                        order.PoNo = model.PoNo;
                        order.RefId = model.RefId;
                        order.Remark = model.Remark;
                        order.SupplierId = model.SupplierId.Value;
                        appDbContext.Orders.Update(order);
                        await appDbContext.SaveChangesAsync();
                        List<OrderDetails> updateOrderDetails = new List<OrderDetails>();
                        List<OrderDetails> addOrderDetails = new List<OrderDetails>();
                        foreach (var item in model.OrderDetails)
                        {
                            if (item.Id.HasValue)
                            {

                                var oldDetails = order.OrderDetails.FirstOrDefault(e => e.Id == item.Id.Value);
                                oldDetails.ProductId = item.ProductId;
                                oldDetails.Qty = item.Qty;
                                updateOrderDetails.Add(oldDetails);
                            }
                            else
                            {
                                var details = new OrderDetails
                                {
                                    OrderId = order.Id,
                                    ProductId = item.ProductId,
                                    Qty = item.Qty
                                };
                                addOrderDetails.Add(details);
                            }
                        }
                        DeleteOrderDetails(order.OrderDetails.ToList(), updateOrderDetails);
                        if (updateOrderDetails.Count > 0)
                            appDbContext.UpdateRange(updateOrderDetails);
                        if (addOrderDetails.Count > 0)
                            await appDbContext.AddRangeAsync(addOrderDetails);
                        await appDbContext.SaveChangesAsync();
                        transaction.Complete();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return order;
        }
        public async Task<Order> GetById(long id)
        {
            return await appDbContext.Orders.Where(e => e.Id == id)
                .Include(e => e.OrderDetails)
                .ThenInclude(e => e.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<List<OrderTableDataViewModel>> GetForTable(int pageNumber, string searchKey, DateTime? date, bool isDate)
        {
            try
            {
                var skipRecord = (pageNumber - 1) * 5;
                if (string.IsNullOrEmpty(searchKey))
                {
                    return await appDbContext.Orders.Skip(skipRecord)
                        .Take(5).Include(e => e.OrderDetails).Include(e => e.Supplier).Select(e => new
                    OrderTableDataViewModel
                        {
                            PoDate = e.PoDate.ToShortDateString(),
                            Supplier = e.Supplier.Name,
                            ExpectedDate = e.ExpectedDate.ToShortDateString(),
                            PoNo = e.PoNo,
                            RefId = e.RefId.ToString(),
                            Id = e.Id
                        }).ToListAsync();
                }
                else
                    return await GetSearchValue(searchKey, skipRecord, date, isDate);
            }
            catch (Exception e)
            {

            }
            return null;
        }
        public async Task<List<OrderTableDataViewModel>> GetSearchValue(string searchKey, int skip, DateTime? date, bool isDate)
        {
            searchKey = searchKey.ToLower();
            Expression<Func<Order, bool>> dataExpression = null;
            if (isDate)
                dataExpression = e => e.ExpectedDate.Date == date.Value.Date || e.PoDate.Date == date.Value.Date 
                || e.PoNo.ToLower().Contains(searchKey) || e.RefId.ToString().Contains(searchKey) 
                || e.Supplier.Name.Contains(searchKey);
            else
                dataExpression = e => e.PoNo.ToLower().Contains(searchKey)
               || e.RefId.ToString().Contains(searchKey) || e.Supplier.Name.Contains(searchKey);
            return await appDbContext.Orders.Include(e => e.Supplier).Where(dataExpression)
                .Skip(skip)
                .Take(5)
                .Select(e => new
                    OrderTableDataViewModel
                {
                    PoDate = e.PoDate.ToShortDateString(),
                    Supplier = e.Supplier.Name,
                    ExpectedDate = e.ExpectedDate.ToShortDateString(),
                    PoNo = e.PoNo,
                    RefId = e.RefId.ToString(),
                    Id = e.Id
                }).ToListAsync();
        }
        Task<List<Order>> IListRepository<Order>.GetAll()
        {
            return appDbContext.Orders.Include(e => e.OrderDetails).ToListAsync();
        }
        public void DeleteOrderDetails(List<OrderDetails> oldOrderDetails, List<OrderDetails> newOrderDetails)
        {
            var details = oldOrderDetails.Where(e => !newOrderDetails.Select(x => x.Id).Contains(e.Id)).ToList();
            if (details.Count > 0)
            {
                appDbContext.OrderDetails.RemoveRange(details);
            }
        }
        public async Task<Order> Delete(long id)
        {
            var order = await appDbContext.Orders.Where(e => e.Id == id)
                .Include(e => e.OrderDetails)
                .FirstOrDefaultAsync();
            if (order != null)
            {
                appDbContext.Remove(order);
                await appDbContext.SaveChangesAsync();
                return order;
            }
            return null;
        }
        public int GetTotalRecordSearchCount(string searchKey, DateTime? date, bool isDate)
        {
            Expression<Func<Order, bool>> dataExpression = null;
            if (isDate)
                dataExpression = e => e.ExpectedDate.Date == date.Value.Date || e.PoDate.Date == date.Value.Date ||
                e.PoNo.ToLower().Contains(searchKey) || e.RefId.ToString().Contains(searchKey) 
                || e.Supplier.Name.Contains(searchKey);
            else
                dataExpression = e => e.PoNo.ToLower().Contains(searchKey)
               || e.RefId.ToString().Contains(searchKey) || e.Supplier.Name.Contains(searchKey);

            return appDbContext.Orders.Include(e => e.Supplier).Where(dataExpression).Count();
        }
        public int GetTotalRecordCount()
        {
            return appDbContext.Orders.Count();
        }
    }
}
