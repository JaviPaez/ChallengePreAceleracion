using System.Collections.Generic;
using WebApplication.Models;

namespace Data.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie Get(int movieId);
        void Insert(Movie movie);
        void Update(Movie movie);
        void Delete(int movieId);
        void Save();
    }
}
