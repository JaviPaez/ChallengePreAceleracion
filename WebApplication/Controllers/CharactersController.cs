using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Services.IConfiguration;
using System.Threading.Tasks;
using Entities.DTO.Incoming;
using System;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CharactersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        [Route("GetCharacters")]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await _unitOfWork.Characters.GetAllAsync();
            return Ok(characters);
        }

        //Get by Id
        [HttpGet]
        [Route("GetCharacter", Name = "GetCharacter")]
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
        [Route("GetCharacterByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var character = await _unitOfWork.Characters.GetCharacterByName(name);
            return Ok(character);
        }

        //Get by age
        [HttpGet]
        [Route("GetCharactersByAge")]
        public async Task<IActionResult> GetByAge(int age)
        {
            var character = await _unitOfWork.Characters.GetCharacterByAge(age);
            return Ok(character);
        }

        //Get by movie
        [HttpGet]
        [Route("GetCharactersByMovie")]
        public async Task<IActionResult> GetByMovie(string movieTitle)
        {
            var character = await _unitOfWork.Characters.GetCharacterByMovie(movieTitle);
            return Ok(character);
        }

        //Update
        [HttpPut]
        public async Task<IActionResult> Put(Character character)
        {
            await _unitOfWork.Characters.UpdateAsync(character);
            await _unitOfWork.SaveAsync();

            return Ok(character);
        }

        //Delete
        [HttpDelete]
        [Route("DeleteCharacter")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Characters.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return Ok();
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