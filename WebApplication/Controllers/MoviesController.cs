using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Services.IConfiguration;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> Post(Movie movie)
        {
            await _unitOfWork.Movies.InsertAsync(movie);
            await _unitOfWork.SaveAsync();

            return CreatedAtRoute("GetMovie", movie.Id, movie);
        }

        //Get all
        [HttpGet]
        [Route("GetMovies")]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync();
            return Ok(movies);
        }

        //Get by Id
        [HttpGet]
        [Route("GetMovie", Name = "GetMovie")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            return Ok(movie);
        }

        //Get by name
        [HttpGet]
        [Route("GetMovieByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var movie = await _unitOfWork.Movies.GetMovieByName(name);
            return Ok(movie);
        }

        //Get by genre
        [HttpGet]
        [Route("GetMovieByGenre")]
        public async Task<IActionResult> GetByGenre(string genreName)
        {
            var movie = await _unitOfWork.Movies.GetMovieByGenre(genreName);
            return Ok(movie);
        }        

        //Update
        [HttpPut]
        public async Task<IActionResult> Put(Movie movie)
        {
            await _unitOfWork.Movies.UpdateAsync(movie);
            await _unitOfWork.SaveAsync();

            return Ok(movie);
        }

        //Delete
        [HttpDelete]
        [Route("DeleteMovie")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitOfWork.Movies.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return Ok();
        }
    }
}