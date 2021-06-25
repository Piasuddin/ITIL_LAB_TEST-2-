using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server.ViewModels
{
    public class OrderPlacedViewModel
    {
        public long? Id { get; set; }
        public int RefId { get; set; }
        [Required]
        [MaxLength(15)]
        public string PoNo { get; set; }
        public DateTime PoDate { get; set; }
        public int SupplierId { get; set; }
        public DateTime ExpectedDate { get; set; }
        public string Remark { get; set; }

        public List<OrderDetailsViewModel> OrderDetails { get; set; }
    }
}
