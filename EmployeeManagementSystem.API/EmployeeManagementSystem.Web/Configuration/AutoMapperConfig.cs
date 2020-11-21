using AutoMapper;
using EmployeeManagementSystem.Core.Models;
using EmployeeManagementSystem.Core.Dto;

namespace EmployeeManagementSystem.Web.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.DOB, opt => opt.MapFrom(opt => opt.DOB.ToString("dd-MMM-yyyy")))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.TotalEmployees, opt => opt.MapFrom(opt => opt.Employees.Count))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(opt => opt.CreatedOn.ToString("dd-MMM-yyyy")));
        }
    }
}