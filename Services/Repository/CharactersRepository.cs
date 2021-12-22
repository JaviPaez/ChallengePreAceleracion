using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
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
            return await dbSet.Where(x => x.Name.Contains(name)).FirstOrDefaultAsync();
        }

        public async Task<List<Character>> GetCharacterByAge(int age)
        {
            return await dbSet.Where(x => x.Age == age).ToListAsync();
        }

        public async Task<List<Character>> GetCharacterByMovie(string movieTitle)
        {
            var character = dbSet.Include(x=> x.Movies.Where(x => x.Title == movieTitle)).ToListAsync();

            return await character;
        }
    }
}