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
    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> Post(UserDTO userDTO)
        {
            var user = new User
            {
                UpdateDate = DateTime.UtcNow,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                DateOfBirth = userDTO.DateOfBirth,
                Country = userDTO.Country               
            };

            await _unitOfWork.Users.InsertAsync(user);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = user.Id },
                UserToDTO(user));
        }

        //Get all
        [HttpGet]
        public async Task<IActionResult> GetCharacters()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            var usersDTO = users
                .Select(user => UserToDTO(user))
                .ToList();

            return Ok(usersDTO);
        }

        //Get by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(UserToDTO(user));
        }       

        //Update
        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            return Ok(UserToDTO(user));
        }

        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool removes = await _unitOfWork.Users.DeleteAsync(id);

            if (!removes)
            {
                return NotFound();
            }
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        private static UserDTO UserToDTO(User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName            
        };
    }
}