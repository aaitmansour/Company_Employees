using Entities.Modeles;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)

        {
        }
        public IEnumerable<Company> GetAllCompanies(bool trackchanges)=>
            FindAll(trackchanges)
            .OrderBy(c => c.name)
            .ToList();
    }
}
