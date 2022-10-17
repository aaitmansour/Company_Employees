using Contracts;
using Entities.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
            { }

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackchanges) =>
            FindByCondition(e => e.CompanyId.Equals(companyId), trackchanges)
            .OrderBy(e => e.Name).ToList();

    }
}
