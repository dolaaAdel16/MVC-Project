using AutoMapper;
using Company.G01.DAL.Models;
using Company.G01.PL.DTOs;

namespace Company.G01.PL.Mapping
{
    //CLR
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile ()
        {
            CreateMap<CreateEmployeeDTO, Employee>().ForMember(d => d.Name, o => o.MapFrom(s => $"{s.Name} "));
            CreateMap<Employee , CreateEmployeeDTO>();
        }
    }
}
