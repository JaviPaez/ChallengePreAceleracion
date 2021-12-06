using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<int> CountAsync()
        {
            var quantity = await _dbContext.Set<T>().CountAsync();

            return quantity;
        }

        public bool EntityExists(int id)
        {
            bool exists;
            if (_dbContext.Set<T>().Find(id) != null) exists = true;
            else exists = false;

            return exists;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity =  _dbContext.Set<T>().Find(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}