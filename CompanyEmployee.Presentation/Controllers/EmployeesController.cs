using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployee.Presentation.Controllers
{
    [Route("api/companies/{companyId/employees}")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service) => _service = service;

        [HttpGet("{id:guid}")]
        public IActionResult GetEmployeesForCompany(Guid companyId, Guid id)
        {
            var employees = _service.EmployeeService.GetEmployee(companyId, id, trackchanges: false);
            return Ok(employees);
        }

    }
}
