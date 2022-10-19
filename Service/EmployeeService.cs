using Contracts;
using Service.Contracts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObject;
using Entities.Exceptions;
using Entities.Modeles;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) 
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackchanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackchanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackchanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackchanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeesDto = _repository.Employee.GetEmployee(companyId, id, trackchanges);
            if (employeesDto is null)
                throw new EmployeeNotFoundException(id);

            var employee = _mapper.Map<EmployeeDto>(employeesDto);
            return employee;
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackchanges)
        {
            var copmpany = _repository.Company.GetCompany(companyId, trackchanges);
            if (copmpany is null)
                throw new CompanyNotFoundException(companyId);

            var employeesFromDb = _repository.Employee.GetEmployees(companyId, trackchanges);
            

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return employeesDto;
        }
    }
}
