using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { set; get; }
        IDepartmentRepository DepartmentRepository { set; get; }

        Task<int> Complete();
    }
}
