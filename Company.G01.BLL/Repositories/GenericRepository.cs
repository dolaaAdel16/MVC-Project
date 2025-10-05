using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Data.Contexts;
using Company.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.BLL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CompanyDbContext _context;
        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if(typeof(TEntity) == typeof(Employee))
            {
                return (IEnumerable<TEntity>)await _context.Employees.Include(E => E.Department).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();   
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            if (typeof(TEntity) == typeof(Employee))
            {
                // Eager Loading
                return await _context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.Id == id) as TEntity;
            }
            return _context.Set<TEntity>().Find(id);
        }

        public async Task AddAsync(TEntity model)
        {
            await _context.AddAsync(model);
            
        }
        public void Update(TEntity model)
        {
            _context.Update(model);
          
        }

        public void Delete(TEntity model)
        {
            _context.Remove(model);    
           
        }

        
    }
}
