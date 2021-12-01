using System.Collections.Generic;
using WebApplication.Models;

namespace Data.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        Movie Get(int MovieId);
        void Insert(Movie movie);
        void Update(Movie movie);
        void Delete(int MovieId);
        void Save();
    }
}