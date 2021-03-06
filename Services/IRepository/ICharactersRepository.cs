using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Services.IRepository
{
    public interface ICharactersRepository : IGenericRepository<Character>
    {
        Task<Character> GetCharacterByName(string name);
        Task<List<Character>> GetCharactersByAge(int age);
        Task<List<Character>> GetCharacterByMovie(string movieTitle);
    }
}
