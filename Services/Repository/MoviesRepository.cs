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
            return await dbSet.Where(movie => movie.Title == title).FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetMovieByGenre(string genreName)
        {
            var genre = _context.Genres.Where(x => x.Name == genreName).FirstOrDefault();

            var movie = dbSet.Include(x => x.Genre).Where(x=>x.Genre==genre).ToListAsync();

            return await movie;
        }

        public async Task<List<Movie>> GetMovieOrderByDate()
        {
            var moviesList = dbSet.ToList();

            return await (Task<List<Movie>>)moviesList.OrderBy(movie => movie.CreationDate);
        }
    }
}