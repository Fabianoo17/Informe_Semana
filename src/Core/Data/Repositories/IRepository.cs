using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        // Write DB
        TEntity Adicionar(TEntity obj);
        TEntity Atualizar(TEntity obj);
        void Remover(int id);

        // Read DB       
        TEntity ObterPorId(int id);
        IEnumerable<TEntity> ObterTodos();
        IEnumerable<TEntity> ObterTodosPaginado(int skip, int take);
        IEnumerable<TEntity> BuscarVarios(Expression<Func<TEntity, bool>> predicate);
        TEntity BuscarUnico(Expression<Func<TEntity, bool>> predicate);

        // Procedures
        PEntity ExecutarProcedure<PEntity>(string procedureName, SqlParameter[] parameters = null);
        IEnumerable<PEntity> ExecutarProcedureList<PEntity>(string procedureName, SqlParameter[] parameters = null);
        void ExecutarProcedureNoQuery(string procedureName, SqlParameter[] parameters = null);

        bool SaveChanges();
    }
}