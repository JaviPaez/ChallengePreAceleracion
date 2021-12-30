using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Services.IConfiguration;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : BaseController
    {
        public GenresController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> Post(Genre genre)
        {
            await _unitOfWork.Genres.InsertAsync(genre);
            await _unitOfWork.SaveAsync();

            return CreatedAtRoute("GetGenre", genre.Id, genre);
        }

        //Read all
        [HttpGet]
        [Route("GetGenres")]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            return Ok(genres);
        }

        //Read by Id
        [HttpGet]
        [Route("GetGenre", Name = "GetGenre")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            return Ok(genre);
        }        

        //Update
        [HttpPut]
        public async Task<IActionResult> Put(Genre genre)
        {
            await _unitOfWork.Genres.UpdateAsync(genre);
            await _unitOfWork.SaveAsync();

            return Ok(genre);
        }

        //Delete
        [HttpDelete]
        [Route("DeleteGenre")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Genres.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return Ok();
        }
    }
}