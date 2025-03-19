using AutoMapper;
using CompanySystem.DAl.Models;
using CompanySystem.PL.ViewModels;

namespace CompanySystem.PL.MapperProfiles
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }
    }
}
