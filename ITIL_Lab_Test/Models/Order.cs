using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }
        public int RefId { get; set; }
        [Required]
        [MaxLength(15)]
        public string PoNo { get; set; }
        public DateTime PoDate { get; set; }
        public DateTime ExpectedDate { get; set; }
        public string Remark { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
