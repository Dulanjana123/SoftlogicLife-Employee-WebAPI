using AutoMapper;
using ProductsAPI.Data;
using WebAPI.Models;

namespace ProductsAPI.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
