using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Entities.Models;
using System.Threading.Tasks;
using Services.IRepository;
using Services.Data;

namespace Services.Repository
{
    public class CharactersRepository : GenericRepository<Character>, ICharactersRepository
    {
        public CharactersRepository(ApplicationDbContext context) : base(context)
        {        
        }

        public async Task<Character> GetCharacterByName(string name)
        {
            return await dbSet.Include(x => x.Movies).ThenInclude(x => x.Genre).Where(x => x.Name.Contains(name)).FirstOrDefaultAsync();
        }

        public async Task<List<Character>> GetCharactersByAge(int age)
        {
            return await dbSet.Include(x => x.Movies).ThenInclude(x => x.Genre).Where(x => x.Age == age).ToListAsync();
        }

        public async Task<List<Character>> GetCharacterByMovie(string movieTitle)
        {
            var movie = _context.Movies.Where(x => x.Title.Contains(movieTitle)).FirstOrDefault();
            
            var characters = await dbSet.Where(x => x.Movies.Contains(movie)).ToListAsync();

            return characters;
        }
    }
}