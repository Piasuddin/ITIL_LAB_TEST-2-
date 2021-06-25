using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.Models
{
    public class OrderDetails
    {
        [Key]
        public long Id { get; set; }
        public decimal Qty { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
