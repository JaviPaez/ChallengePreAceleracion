using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Models;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class CharacterRepository : GenericRepository<Character> , ICharacterRepository
    { 
        public CharacterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {            
        }

        //Methods
        public async Task<Character> GetCharacterByName(string name)
        {
            return await _dbContext.Characters.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Character>> GetCharacterByAge(int age)
        {
            return await _dbContext.Characters.Where(x => x.Age == age).ToListAsync();
        }

        public async Task<List<Character>> GetCharacterByMovie(string movieTitle)
        {
            var movie = _dbContext.Movies.Where(x => x.Title == movieTitle).FirstOrDefault();

            return await _dbContext.Characters.Where(x => x.Movies == movie).ToListAsync();
        }

        //
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}