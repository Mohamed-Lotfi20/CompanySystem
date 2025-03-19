using CompanySystem.BLL.Interfaces;
using CompanySystem.DAl.Contexts;
using CompanySystem.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext dbContext;
        public EmployeeRepository(CompanyDbContext _dbContext):base(_dbContext)
            => this.dbContext = _dbContext;

        public IQueryable<Employee>GetEmployeeByAddress(string _Address)
            => dbContext.employees.Where(E => E.Address.ToLower() == _Address.ToLower());

        public IQueryable<Employee> GetEmployeeByName(string name)
            => dbContext.employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));
           
        public void DeleteDeptIDFromEmployee(int _id)
        {
            var Emp = dbContext.employees.FirstOrDefault(e=>e.DeptID==_id);
            Emp.DeptID = null;
            dbContext.SaveChanges();
        }
    }
}
