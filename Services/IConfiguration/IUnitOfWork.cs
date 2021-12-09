using Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IConfiguration
{
    public interface IUnitOfWork
    {
        ICharactersRepository Characters { get; }
        IMoviesRepository Movies { get; }
        IGenresRepository Genres { get; }

        Task SaveAsync();
    }
}