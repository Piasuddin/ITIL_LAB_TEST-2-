using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.ViewModels
{
    public class OrderDetailsCreateViewModel
    {
        public long? Id { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
    }
}
