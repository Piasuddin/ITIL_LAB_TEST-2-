using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.ViewModels
{
    public class ReportViewModel
    {
        public string PoNo { get; set; }
        public string PoDate { get; set; }
        public string ExpectedDate { get; set; }
        public string Remark { get; set; }
        public string Supplier { get; set; }
        public string RefId { get; set; }
        public string Subtotal { get; set; }
        public List<OrderedProductsViewModel> Products { get; set; }
    }
    public class OrderedProductsViewModel
    {
        public string Total { get; set; }
        public string Name { get; set; }
        public string Rate { get; set; }
        public string Qty { get; set; }
    }
}
