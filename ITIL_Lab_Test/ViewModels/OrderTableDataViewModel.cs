using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.ViewModels
{
    public class OrderTableDataViewModel
    {
        [Display(Name = "PO. DATE")]
        public string PoDate { get; set; }
        [Display(Name = "SUPPLIER")]
        public string Supplier { get; set; }
        [Display(Name = "EX. DATE")]
        public string ExpectedDate { get; set; }
        [Display(Name = "PO. NO")]
        public string PoNo { get; set; }
        [Display(Name = "REF. ID")]
        public string RefId { get; set; }
        public long Id { get; set; }
    }
}
