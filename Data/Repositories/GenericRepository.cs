using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class where TContext : DbContext
    {
        private readonly TContext _dbContext;

        protected GenericRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public TEntity Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public TEntity Update(int id) //(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}