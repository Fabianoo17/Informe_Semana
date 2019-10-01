using Core.Data.Context;
using Core.Data.Interfaces;
using Core.Models;

namespace Core.Data.Repositories
{
    public class RepositoryT004_SEMANA : Repository<T004_SEMANA>, IRepositoryT004_SEMANA
    {
        public RepositoryT004_SEMANA()
            : base(new InformeSemanaContext())
        {

        }
    }
}