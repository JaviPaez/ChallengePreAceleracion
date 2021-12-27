using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Entities.Models;
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

        public async Task<Movie> GetMovieByName(string movieTitle)
        {
            return await dbSet.Where(movie => movie.Title.Contains(movieTitle)).Include(x => x.Genre).Include(x => x.Characters).FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetMovieByGenre(string genreName)
        {
            var genre = _context.Genres.Where(x => x.Name.Contains(genreName)).FirstOrDefault();

            var movie = await dbSet.Include(x => x.Genre).Where(x => x.Genre == genre).Include(x => x.Characters).ToListAsync();

            return movie;
        }

        public async Task<List<Movie>> GetMovieOrderByDate(bool Ascending)
        {
            if (Ascending == true)
                return await dbSet.OrderBy(movie => movie.CreationDate).Include(x => x.Genre).Include(x => x.Characters).ToListAsync();

            else return await dbSet.OrderByDescending(movie => movie.CreationDate).Include(x => x.Genre).Include(x => x.Characters).ToListAsync();
        }
    }
}