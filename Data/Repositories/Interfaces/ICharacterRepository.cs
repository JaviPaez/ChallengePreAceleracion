using System.Collections.Generic;
using WebApplication.Models;

namespace Data.Repositories
{
    public interface ICharacterRepository
    {
        IEnumerable<Character> GetAll();
        Character Get(int CharacterId);
        void Insert(Character character);
        void Update(Character character);
        void Delete(int CharacterId);
        void Save();
    }
}