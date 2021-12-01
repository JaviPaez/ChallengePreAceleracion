using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public List<T> GetAll();
        public T Get(int id);
        public T Add(T entity);
        public T Update(int id);
        public T Delete(int id);
    }
}