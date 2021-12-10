using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using System.Threading.Tasks;
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