using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Services.IConfiguration;
using System.Threading.Tasks;
using Entities.DTO.Incoming;
using System;
using System.Linq;

namespace WebApplication.Controllers
{
    public class CharactersController : BaseController
    {
        public CharactersController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> Post(CharacterDTO characterDTO)
        {
            var character = new Character
            {
                Image = characterDTO.Image,
                Name = characterDTO.Name,
                Age = characterDTO.Age,
                Weight = characterDTO.Weight,
                Story = characterDTO.Story,
                Movies = characterDTO.Movies,
                UpdateDate = DateTime.UtcNow
            };

            await _unitOfWork.Characters.InsertAsync(character);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(
                nameof(GetCharacterById),
                new { id = character.Id },
                CharacterToDTO(character));
        }

        //Get all
        [HttpGet]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await _unitOfWork.Characters.GetAllAsync();

            var charactersDTO = characters
                .Select(character => CharacterToDTO(character))
                .ToList();

            return Ok(charactersDTO);
        }

        //Get by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            var character = await _unitOfWork.Characters.GetByIdAsync(id);

            if (character == null)
            {
                return NotFound();
            }
            return Ok(CharacterToDTO(character));
        }

        //Get by name
        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var character = await _unitOfWork.Characters.GetCharacterByName(name);

            if (character == null)
            {
                return NotFound();
            }
            return Ok(CharacterToDTO(character));
        }

        //Get by age
        [HttpGet]
        [Route("age/{age}")]
        public async Task<IActionResult> GetByAge(int age)
        {
            var characters = await _unitOfWork.Characters.GetCharactersByAge(age);

            var charactersDTO = characters
                .Select(character => CharacterToDTO(character))
                .ToList();

            if (charactersDTO.Count < 1)
            {
                return NotFound();
            }
            return Ok(charactersDTO);
        }

        //Get by movie
        [HttpGet]
        [Route("movie/{movie}")]
        public async Task<IActionResult> GetByMovie(string movie)
        {
            var characters = await _unitOfWork.Characters.GetCharacterByMovie(movie);

            var charactersDTO = characters
                .Select(character => CharacterToDTO(character))
                .ToList();

            if (charactersDTO.Count < 1)
            {
                return NotFound();
            }
            return Ok(charactersDTO);
        }

        //Update
        [HttpPut]
        public async Task<IActionResult> Put(Character character)
        {
            await _unitOfWork.Characters.UpdateAsync(character);
            await _unitOfWork.SaveAsync();

            return Ok(CharacterToDTO(character));
        }

        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool removes = await _unitOfWork.Characters.DeleteAsync(id);

            if (!removes)
            {
                return NotFound();
            }
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        private static CharacterDTO CharacterToDTO(Character character) => new()
        {
            Id = character.Id,
            Image = character.Image,
            Name = character.Name,
            Age = character.Age,
            Weight = character.Weight,
            Story = character.Story,
            Movies = character.Movies,
        };
    }
}