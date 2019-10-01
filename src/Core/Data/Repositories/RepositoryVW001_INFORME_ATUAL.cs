using Core.Data.Context;
using Core.Data.Interfaces;
using Core.Models;

namespace Core.Data.Repositories
{
    public class RepositoryVW001_INFORME_ATUAL : Repository<VW001_INFORME_ATUAL>, IRepositoryVW001_INFORME_ATUAL 
    {
        public RepositoryVW001_INFORME_ATUAL()
            : base(new InformeSemanaContext())
        {

        }
    }
}