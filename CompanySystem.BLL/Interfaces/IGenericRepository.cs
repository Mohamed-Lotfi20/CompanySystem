using CompanySystem.DAl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll();  // Only Return Data => IEnumerable
        Task<T> GetByID(int? id);
        Task Add(T Item); 
        void Delete(T Item);
        void Update(T Item);
    }
}
