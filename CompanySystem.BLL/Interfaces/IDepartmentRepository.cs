using CompanySystem.DAl.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BLL.Interfaces
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
        public IQueryable<Department> GetDepartmentByName(string name);
    }
}
