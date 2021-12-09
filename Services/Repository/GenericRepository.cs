using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<bool> InsertAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(T entity)
        {            
            dbSet.Update(entity);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);

            dbSet.Remove(entity);

            return true;
        }

        public async Task<int> CountAsync()
        {
            return await dbSet.CountAsync();
        }

        public bool EntityExists(int id)
        {
            bool exists;
            if (dbSet.Find(id) != null) exists = true;
            else exists = false;

            return exists;
        }
    }
}