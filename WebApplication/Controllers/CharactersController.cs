using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route(template: "api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public IActionResult Post(Character character)
        {
            _context.Characters.Add(character);
            _context.SaveChanges();
            return Ok(_context.Characters.ToList());
        }

        //Read
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Characters.ToList());
        }

        //Update
        [HttpPut]
        public IActionResult Put(Character character)
        {
            if (_context.Characters.FirstOrDefault(x => x.CharacterId == character.CharacterId) == null) return BadRequest("The character sent doesn't exist.");
            else
            {
                var internalCharacter = _context.Characters.Find(character.CharacterId);

                internalCharacter.Name = character.Name;
                internalCharacter.Image = character.Image;
                internalCharacter.Age = character.Age;
                internalCharacter.Weight = character.Weight;
                internalCharacter.Story = character.Story;

                _context.SaveChanges();
            }

            return Ok(_context.Characters.ToList());
        }
        
        //Delete
[HttpDelete]
[Route("{id}")]
public IActionResult Delete(int characterId)
{
if (_context.Characters.FirstOrDefault(x => x.CharacterId == characterId) == null) return BadRequest("The character sent doesn't exist.");
else
{
var internalCharacter = _context.Characters.Find(character.CharacterId);

_context.Characters.Remove(internalCharacter);

_context.SaveChanges();
}
return Ok(_context.Characters.ToList());
}
    }
}
