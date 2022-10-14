using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployee.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesControllor : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesControllor(IServiceManager service) =>
            _service = service;

        [HttpGet("{id:Guid}")]
        public IActionResult GetCompanies()
        {
            //throw new Exception("excptin");
            var companies = _service.CompanyService.GetAllCompanies(trackchanges: false);
            return Ok(companies);
        }
    }
}
