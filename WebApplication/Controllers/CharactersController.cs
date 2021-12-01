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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Characters.ToList());
        }

    }
}