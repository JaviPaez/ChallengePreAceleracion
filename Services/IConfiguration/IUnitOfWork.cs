using Services.IRepository;
using System.Threading.Tasks;

namespace Services.IConfiguration
{
    public interface IUnitOfWork
    {
        ICharactersRepository Characters { get; }
        IMoviesRepository Movies { get; }
        IGenresRepository Genres { get; }
        IUsersRepository Users { get; }

        Task SaveAsync();
    }
}