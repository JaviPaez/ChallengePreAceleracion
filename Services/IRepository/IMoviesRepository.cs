using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.IRepository
{
    public interface IMoviesRepository : IGenericRepository<Movie>
    {
        Task<Movie> GetMovieByName(string name);
        Task<List<Movie>> GetMovieByGenre(string genre);
        Task<List<Movie>> GetMovieOrderByDate(DateTime date);
    }
}