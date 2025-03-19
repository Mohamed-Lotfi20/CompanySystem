using CompanySystem.BLL.Interfaces;
using CompanySystem.DAl.Contexts;
using CompanySystem.DAl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly CompanyDbContext dbContext;
        public DepartmentRepository(CompanyDbContext _dbContext) : base(_dbContext)
            => this.dbContext = _dbContext;

        public IQueryable<Department> GetDepartmentByName(string name)
            => dbContext.departments.Where(E => E.Name.ToLower().Contains(name.ToLower()));
    }
}
