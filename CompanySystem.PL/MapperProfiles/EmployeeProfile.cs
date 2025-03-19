using AutoMapper;
using CompanySystem.DAl.Models;
using CompanySystem.PL.ViewModels;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;

namespace CompanySystem.PL.MapperProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap();
        }
    }
}
