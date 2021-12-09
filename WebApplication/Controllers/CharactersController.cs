using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Services.IConfiguration;
using System.Threading.Tasks;

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
        //[Route("{character.Id}")]
        public async Task<IActionResult> Post(Character character)
        {
            await _unitOfWork.Characters.InsertAsync(character);
            await _unitOfWork.SaveAsync();

            return CreatedAtRoute("GetCharacter", character.Id, character);
        }

        //Read all
        [HttpGet]
        [Route("GetCharacters")]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await _unitOfWork.Characters.GetAllAsync();
            return Ok(characters);
        }

        //Read by Id
        [HttpGet]
        [Route("GetCharacter", Name = "GetCharacter")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            var character = await _unitOfWork.Characters.GetByIdAsync(id);
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
    }
}