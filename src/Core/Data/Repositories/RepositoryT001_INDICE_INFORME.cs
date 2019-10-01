using Core.Data.Context;
using Core.Data.Interfaces;
using Core.Models;

namespace Core.Data.Repositories
{
    public class RepositoryT001_INDICE_INFORME : Repository<T001_INDICE_INFORME>, IRepositoryT001_INDICE_INFORME
    {

        public RepositoryT001_INDICE_INFORME()
            : base(new InformeSemanaContext())
        {

        }

    }
}