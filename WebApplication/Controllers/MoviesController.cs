using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Services.IConfiguration;
using System.Threading.Tasks;
using Entities.DTO.Incoming;
using System;
using System.Linq;

namespace WebApplication.Controllers
{
    public class MoviesController : BaseController
    {
        public MoviesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> Post(MovieDTO movieDTO)
        {
            var movie = new Movie
            {
                Image = movieDTO.Image,
                Title = movieDTO.Title,
                CreationDate = movieDTO.CreationDate,
                Score = movieDTO.Score,
                UpdateDate = DateTime.UtcNow
            };

            await _unitOfWork.Movies.InsertAsync(movie);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(
                nameof(GetMovieById),
                new { id = movie.Id },
                MovieToDTO(movie));
        }

        //Get all
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync();

            var moviesDTO = movies
                .Select(movie => MovieToDTO(movie))
                .ToList();

            return Ok(moviesDTO);
        }

        //Get by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            return Ok(MovieToDTO(movie));
        }

        //Get by name
        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var movie = await _unitOfWork.Movies.GetMovieByName(name);

            if (movie == null)
            {
                return NotFound();
            }
            return Ok(MovieToDTO(movie));
        }

        //Get by genre
        [HttpGet]
        [Route("genre/{genre}")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            var movies = await _unitOfWork.Movies.GetMovieByGenre(genre);

            var moviesDTO = movies
                .Select(movie => MovieToDTO(movie))
                .ToList();

            if (moviesDTO.Count < 1)
            {
                return NotFound();
            }
            return Ok(moviesDTO);
        }


        //Get all ordered by date ascending or descending
        [HttpGet]
        [Route("order/{ASC}")]
        public async Task<IActionResult> GetOrderByDate(string ASC)
        {
            var movies = await _unitOfWork.Movies.GetMovieOrderByDate(ASC);

            var moviesDTO = movies
                .Select(movie => MovieToDTO(movie))
                .ToList();

            if (moviesDTO.Count < 1)
            {
                return NotFound();
            }
            return Ok(moviesDTO);
        }

        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, MovieDTO movieDTO)
        {
            if (id != movieDTO.Id)
            {
                return BadRequest();
            }

            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.UpdateDate = DateTime.UtcNow;
            movie.Image = movieDTO.Image;
            movie.Title = movieDTO.Title;
            movie.CreationDate = movieDTO.CreationDate;
            movie.Score = movieDTO.Score;
            movie.Characters = movieDTO.Characters;


            await _unitOfWork.SaveAsync();

            return Ok(MovieToDTO(movie));
        }

        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool removes = await _unitOfWork.Movies.DeleteAsync(id);

            if (!removes)
            {
                return NotFound();
            }
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        private static MovieDTO MovieToDTO(Movie movie) => new()
        {
            Id = movie.Id,
            Image = movie.Image,
            Title = movie.Title,
            CreationDate = movie.CreationDate,
            Score = movie.Score,
            Genre = movie.Genre,
            Characters = movie.Characters,
        };
    }
}