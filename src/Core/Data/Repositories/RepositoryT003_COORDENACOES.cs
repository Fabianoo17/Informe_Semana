using Core.Data.Context;
using Core.Data.Interfaces;
using Core.Models;

namespace Core.Data.Repositories
{
    public class RepositoryT003_COORDENACOES : Repository<T003_COORDENACOES>, IRepositoryT003_COORDENACOES
    {
        public RepositoryT003_COORDENACOES()
            : base(new InformeSemanaContext())
        {

        }

    }
}