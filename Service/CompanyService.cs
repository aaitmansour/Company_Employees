using Contracts;
using Entities.Modeles;
using Service.Contracts;
using Shared.DataTransferObject;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager Logger, IMapper mapper)
        {
            _repository = repository;
            _logger = Logger;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackchanges)
        {
            var companies = _repository.Company.GetAllCompanies(trackchanges);
            var comapaniesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return comapaniesDto;
        }

        
    }
}
