using AspNetCore.Reporting;
using ITIL_Lab_Test.Models;
using ITIL_Lab_Test.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IOrderRepository orderRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.orderRepository = orderRepository;
            this.webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    string mimtype = "";
        //    int extension = 1;
        //    var path = $"{webHostEnvironment.WebRootPath}\\Reports\\OrderDetailsReport.rdlc";
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("PoDate", "ASP.NET CORE RDLC Report");
        //    //get products from product table 
        //    //var produts = await productRepository.GetAll();
        //    LocalReport localReport = new LocalReport(path);
        //    //localReport.AddDataSource("DataSet1", produts);
        //    //localReport.AddDataSource("DataSet2", produts);
        //    var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
        //    byte[] bytes = result.MainStream;
        //    string fileName = "Report.pdf";
        //    return File(bytes, "application/pdf", fileName);
        //    //return View(new List<Order>());
        //}
        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("search/{searchkey}")]
        public async Task<IActionResult> SearchItem(string searchkey)
        {
            return View();
        }
    }
}
