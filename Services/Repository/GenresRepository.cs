using Domain.Models;
using Services.IRepository;
using Services.Data;

namespace Services.Repository
{
    public class GenresRepository : GenericRepository<Genre>, IGenresRepository
    {
        public GenresRepository(ApplicationDbContext context) : base(context)
        {        
        }
    }
}