using Core.Data.Context;
using Core.Data.Interfaces;
using Core.Models;

namespace Core.Data.Repositories
{
    public class RepositoryT005_PRORROGA_INFORME : Repository<T005_PRORROGA_INFORME> , IRepositoryT005_PRORROGA_INFORME
    {
        public RepositoryT005_PRORROGA_INFORME()
            : base(new InformeSemanaContext())
        {

        }
    }
}