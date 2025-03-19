using System;

namespace CompanySystem.PL.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }

        public RoleViewModel()
        {
            this.Id =Guid.NewGuid().ToString();
        }
    }
}
