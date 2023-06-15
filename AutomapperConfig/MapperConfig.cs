using APIAssignment2.Models.Entities;
using AutoMapper;

namespace APIAssignment2.AutomapperConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Employee,
                EmployeeBase>().ReverseMap();
            CreateMap<Department,
                DepartmentBase>().ReverseMap();

        }
    }
}
