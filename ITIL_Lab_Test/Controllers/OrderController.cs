using AspNetCore.Reporting;
using ITIL_Lab_Test.Helper;
using ITIL_Lab_Test.Models;
using ITIL_Lab_Test.Repositories;
using ITIL_Lab_Test.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using server.Repositories;
using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductRepository productRepository;
        private readonly IReportRepository reportRepository;
        private readonly ISupplierRepository supplierRepository;

        public OrderController(IOrderRepository orderRepository, IWebHostEnvironment webHostEnvironment,
            IProductRepository productRepository, IReportRepository reportRepository,
            ISupplierRepository supplierRepository)
        {
            this.orderRepository = orderRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.productRepository = productRepository;
            this.reportRepository = reportRepository;
            this.supplierRepository = supplierRepository;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchKey, int data = 1)
        {
            bool isDate = false;
            DateTime? dateTime = null;
            if (AppHelper.CheckIfDate(searchKey))
            {
                var date = AppHelper.GetDate(searchKey);
                if(date != null)
                {
                    dateTime = date;
                    isDate = true;
                }
            }
            var totalRecord = string.IsNullOrEmpty(searchKey)? orderRepository.GetTotalRecordCount()
                : orderRepository.GetTotalRecordSearchCount(searchKey, dateTime, isDate);
            ViewBag.tatalPage = Math.Round((totalRecord / 5M) + (decimal).4);
            ViewBag.page = data;
            ViewBag.searchKey = searchKey;
            var items = await orderRepository.GetForTable(data, searchKey, dateTime, isDate);
            
            return View(items);
        }
        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            OrderCreateViewModel model = new OrderCreateViewModel();
            model.RefId = orderRepository.GetTotalRecordCount() + 1;
            var data = await supplierRepository.GetAll();
            var returnData = data.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            model.Suppliers = returnData;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Order result = null;
                if (model.Id > 0)
                {
                    result = await orderRepository.Update(model);
                    TempData["Success"] = "Order update successfull";
                }
                else
                {
                    result = await orderRepository.Add(model);
                    TempData["Success"] = "Order created successfull";
                }
                if (result != null)
                    return RedirectToAction("index");
            }
            return View(model);
        }
        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await orderRepository.GetById(id);
            if (result != null)
                return Ok(result);
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await orderRepository.GetById(id);
            OrderCreateViewModel model = new OrderCreateViewModel() { 
                Id = result.Id,
                ExpectedDate = result.ExpectedDate,
                PoDate = result.PoDate,
                PoNo = result.PoNo,
                RefId = result.RefId,
                Remark = result.Remark,
                SupplierId = result.SupplierId
            };
            var data = await supplierRepository.GetAll();
            var returnData = data.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            model.Suppliers = returnData;
            model.OrderDetails = result.OrderDetails.Select(e => new OrderDetailsCreateViewModel
            {
                Id = e.Id,
                ProductId = e.ProductId,
                ProductName = e.Product.Name,
                Qty = e.Qty,
                Rate = e.Product.Rate
            }).ToList();
            return View("AddOrder", model);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var result = await orderRepository.Delete(id);
                if (result != null)
                {
                    TempData["Success"] = "Order deleted successfull";
                }
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> PrintReport(long id)
        {
            try
            {
                var data = await reportRepository.GetReportByOrderId(id);
                if (data != null)
                {
                    string mimtype = "";
                    int extension = 1;
                    var path = $"{webHostEnvironment.WebRootPath}\\Reports\\OrderDetailsReport.rdlc";
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("PoDate", data.PoDate);
                    parameters.Add("poNo", data.PoNo);
                    parameters.Add("refId", data.RefId);
                    parameters.Add("remark", !string.IsNullOrEmpty(data.Remark)? data.Remark: "[not imported]");
                    parameters.Add("supplier", data.Supplier);
                    parameters.Add("exDate", data.ExpectedDate);
                    parameters.Add("subTotal", data.Subtotal);
                    LocalReport localReport = new LocalReport(path);
                    localReport.AddDataSource("DataSet3", data.Products);
                    var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
                    byte[] bytes = result.MainStream;
                    string fileName = "Report.pdf";
                    return File(bytes, "application/pdf", fileName);
                }
                return NotFound();
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("index");
        }
    }
}
