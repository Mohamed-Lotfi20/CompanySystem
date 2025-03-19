using AutoMapper;
using CompanySystem.DAl.Models;
using CompanySystem.PL.ViewModels;

namespace CompanySystem.PL.MapperProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
        }
    }
}
