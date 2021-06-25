using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.ViewModels
{
    public class OrderDetailsViewModel
    {
        public long? Id { get; set; }
        public long ProductId { get; set; }
        public decimal Qty { get; set; }

        public long OrderId { get; set; }
    }
}
