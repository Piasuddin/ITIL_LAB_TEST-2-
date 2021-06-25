using ITIL_Lab_Test.Models;
using ITIL_Lab_Test.ViewModels;
using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.Repositories
{
    public interface IOrderRepository: IListRepository<Order>
    {
        Task<Order> Add(OrderCreateViewModel model);
        Task<List<OrderTableDataViewModel>> GetForTable(int pageNumber, string searchKey, DateTime? date, bool isDate);
        Task<Order> GetById(long id);
        Task<Order> Update(OrderCreateViewModel model);
        Task<Order> Delete(long id);
        int GetTotalRecordCount();
        int GetTotalRecordSearchCount(string searchKey, DateTime? date, bool isDate);
    }
}
