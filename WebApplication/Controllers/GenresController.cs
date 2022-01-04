using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Services.IConfiguration;
using System.Threading.Tasks;
using Entities.DTO.Incoming;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplication.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenresController : BaseController
    {
        public GenresController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> Post(GenreDTO genreDTO)
        {
            var genre = new Genre
            {
                Name = genreDTO.Name,
                Image = genreDTO.Image,             
                UpdateDate = DateTime.UtcNow
            };

            await _unitOfWork.Genres.InsertAsync(genre);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(
                nameof(GetGenreById),
                new { id = genre.Id },
                GenreToDTO(genre));
        }

        //Get all
        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();

            var genresDTO = genres
                .Select(genre => GenreToDTO(genre))
                .ToList();

            return Ok(genresDTO);
        }

        //Get by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }
            return Ok(GenreToDTO(genre));
        }        

        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, GenreDTO genreDTO)
        {
            if (id != genreDTO.Id)
            {
                return BadRequest();
            }

            var genre = await _unitOfWork.Genres.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            genre.UpdateDate = DateTime.UtcNow;
            genre.Image = genreDTO.Image;
            genre.Name = genreDTO.Name;          


            await _unitOfWork.SaveAsync();

            return Ok(GenreToDTO(genre));
        }

        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool removes = await _unitOfWork.Genres.DeleteAsync(id);

            if (!removes)
            {
                return NotFound();
            }
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        private static GenreDTO GenreToDTO(Genre genre) => new()
        {
            Id = genre.Id,
            Image = genre.Image,
            Name = genre.Name            
        };
    }
}