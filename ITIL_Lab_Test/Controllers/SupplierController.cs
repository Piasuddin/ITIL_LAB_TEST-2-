using ITIL_Lab_Test.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SupplierController>))]
        public async Task<IActionResult> Get()
        {
            return Ok(await supplierRepository.GetAll());
        }
    }
}
