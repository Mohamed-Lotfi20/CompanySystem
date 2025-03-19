using AutoMapper;
using CompanySystem.DAl.Models;
using CompanySystem.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CompanySystem.PL.MapperProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel,IdentityRole>()
                .ForMember(d=>d.Name, o=>o.MapFrom(s=>s.RoleName))
                .ReverseMap();
        }
    }
}
