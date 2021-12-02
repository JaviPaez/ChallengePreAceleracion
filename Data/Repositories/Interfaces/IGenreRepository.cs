using System.Collections.Generic;
using WebApplication.Models;

namespace Data.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();
        Genre Get(int genreId);
        void Insert(Genre genre);
        void Update(Genre genre);
        void Delete(int genreId);
        void Save();
    }
}
