using Entities.Models;
using Services.IRepository;
using Services.Data;

namespace Services.Repository
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {        
        }        
    }
}