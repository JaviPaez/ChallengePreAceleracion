using System.Collections.Generic;
using Domain.Models;

namespace Services.Interfaces
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
