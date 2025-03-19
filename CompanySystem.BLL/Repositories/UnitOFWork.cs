using CompanySystem.BLL.Interfaces;
using CompanySystem.DAl.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BLL.Repositories
{
    public class UnitOFWork : IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public CompanyDbContext dbcontext { get; }
        public UnitOFWork(CompanyDbContext _dbContext) { 
            this.DepartmentRepository = new DepartmentRepository(_dbContext);
            this.EmployeeRepository = new EmployeeRepository(_dbContext);
            dbcontext = _dbContext;
        }

        public async Task<int> Complete()
            => await dbcontext.SaveChangesAsync();

        public  async void Dispose()
            => await dbcontext.DisposeAsync();
    }
}
