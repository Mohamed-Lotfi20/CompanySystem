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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext dbContext;
        public GenericRepository(CompanyDbContext _dbContext)
            => dbContext = _dbContext;


        public async Task Add(T Item)
            => await dbContext.Set<T>().AddAsync(Item);

        public void Delete(T Item)
            => dbContext.Set<T>().Remove(Item);

        public async Task<IEnumerable<T>> GetAll()
        { 
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await dbContext.employees.Include(e=>e.department).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        } 

        public async Task<T> GetByID(int? id)
            => await dbContext.Set<T>().FindAsync(id);

        public void Update(T Item)
            => dbContext.Set<T>().Update(Item);

    }
}
