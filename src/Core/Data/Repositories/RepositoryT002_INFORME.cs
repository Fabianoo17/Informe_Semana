using Core.Data.Context;
using Core.Data.Interfaces;
using Core.Models;

namespace Core.Data.Repositories
{
    public class RepositoryT002_INFORME : Repository<T002_INFORME>, IRepositoryT002_INFORME
    {
        public RepositoryT002_INFORME()
            : base(new InformeSemanaContext())
        {

        }

    }
}