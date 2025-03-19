using CompanySystem.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        public IQueryable<Employee> GetEmployeeByName(string _name);
        public IQueryable<Employee> GetEmployeeByAddress(string _address);
        public void DeleteDeptIDFromEmployee(int _id);

        // IQueryable => The best collection for filter data
        //  هترجع الداتا متفلتره جاهزه IQueryable 

        // IEnumerable => The best collection for Get data
        // هتجع الداتا الاول ثم هتفلترهم IEnumerable
    }
}
