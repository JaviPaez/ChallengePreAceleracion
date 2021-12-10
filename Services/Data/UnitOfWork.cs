using Services.IConfiguration;
using Services.IRepository;
using Services.Repository;
using System;
using System.Threading.Tasks;

namespace Services.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ICharactersRepository Characters { get; private set; }

        public IMoviesRepository Movies { get; private set; }

        public IGenresRepository Genres { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Characters = new CharactersRepository(context);
            //Movies = new MoviesRepository(context);
            //Genres = new GenresRepository(context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}