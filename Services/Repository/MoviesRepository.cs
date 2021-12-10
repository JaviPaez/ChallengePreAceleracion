using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using System.Threading.Tasks;
using Services.IRepository;
using Services.Data;

namespace Services.Repository
{
    public class MoviesRepository : GenericRepository<Movie>, IMoviesRepository
    {
        public MoviesRepository(ApplicationDbContext context) : base(context)
        {    
        }

        public async Task<Movie> GetMovieByName(string title)
        {
            return await dbSet.Where(x => x.Title == name).FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetMovieByGenre(string genreName)
        {
            var genre = _context.Genres.Where(x => x.Name == genreName).FirstOrDefaultAsync();

            return await dbSet.Where(x => x.Genres == genre).ToListAsync();
        }

        public async Task<List<Movie>> GetMovieOrderByDate()
        {
            var movie = _context.Movies.Where(x => x.Title == movieTitle).FirstOrDefaultAsync();

            var moviesList = dbSet.Where(x => x.Movies == movie).ToListAsync();
	    
 	    return await moviesList.OrderBy(movie => movie.CreationDate);
        }
    }
}