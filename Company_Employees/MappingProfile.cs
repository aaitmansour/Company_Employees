using AutoMapper;
using Entities.Modeles;
using Shared.DataTransferObject;

namespace Company_Employees
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Company, CompanyDto>()
                .ForCtorParam("fulladresse",
                    opt => opt.MapFrom(x => string.Join(' ', x.adresse, x.country)));

            CreateMap<Employee, EmployeeDto>();
        }
    }
}
