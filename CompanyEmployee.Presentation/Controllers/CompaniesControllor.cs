using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject;

namespace CompanyEmployee.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesControllor : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesControllor(IServiceManager service) =>
            _service = service;

        [HttpGet("{id:Guid}", Name = "CompanyById")]
        public IActionResult GetCompanies()
        {
            //throw new Exception("excptin");
            var companies = _service.CompanyService.GetAllCompanies(trackchanges: false);
            return Ok(companies);
        }


        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");

            var createdCompany = _service.CompanyService.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.id }, createdCompany);
        }


        [HttpGet("collection/({ids})", Name ="CompanyCollection")]
        public IActionResult GetCompanyCollection(IEnumerable<Guid> ids)
        {
            var compamies = _service.CompanyService.GetByIds(ids, trackChanges: false);
            return Ok(compamies);
        }


        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companycollection)
        {
            var result = _service.CompanyService.CreateCompanyCollection(companycollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
    }
}
