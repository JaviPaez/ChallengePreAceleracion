using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext()
        //{
        //}

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Entities
        public DbSet<Character> Characters { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}