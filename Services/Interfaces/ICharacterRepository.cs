using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Interfaces
{
    public interface ICharacterRepository : IGenericRepository<Character>
    {
        Task<Character> GetCharacterByName(string name);
        Task<List<Character>> GetCharacterByAge(int age);
        Task<List<Character>> GetCharacterByMovie(string movieTitle);
    }
}
