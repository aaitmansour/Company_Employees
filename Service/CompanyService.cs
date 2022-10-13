using Contracts;
using Entities.Modeles;
using Service.Contracts;
using Shared.DataTransferObject;
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

        public CompanyService(IRepositoryManager repository, ILoggerManager Logger)
        {
            _repository = repository;
            _logger = Logger;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackchanges)
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackchanges);
                var comapaniesDto = companies.Select(c => new CompanyDto(c.id, c.name ?? "", String.Join(' ', c.adresse, c.country))).ToList();
                return comapaniesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the{nameof(GetAllCompanies)} service meyhod{ex}");
                throw;
            }
        }

        
    }
}
