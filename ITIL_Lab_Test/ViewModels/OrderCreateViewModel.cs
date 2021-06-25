using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.ViewModels
{
    public class OrderCreateViewModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "REF. ID")]
        public int RefId { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "PO. NO")]
        public string PoNo { get; set; }

        [Required]
        [Display(Name = "PO. DATE")]
        [DataType(DataType.Date)]
        public DateTime? PoDate { get; set; }

        [Required]
        [Display(Name = "SUPPLIER")]
        public int? SupplierId { get; set; }

        [Required]
        [Display(Name = "EXPECTED DATE")]
        [DataType(DataType.Date)]
        public DateTime? ExpectedDate { get; set; }

        [Display(Name = "REMARK")]
        public string Remark { get; set; }
        public string Item { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Rate { get; set; }
        public List<OrderDetailsCreateViewModel> OrderDetails { get; set; } = new List<OrderDetailsCreateViewModel>();
        public List<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
    }
}
