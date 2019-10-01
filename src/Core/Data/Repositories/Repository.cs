using Core.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected DbContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(DbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public TEntity Adicionar(TEntity obj)
        {
            var objReturn = DbSet.Add(obj);
            return objReturn;
        }

        public TEntity Atualizar(TEntity obj)
        {
            var entry = Db.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;
            return obj;
        }

        public void Remover(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public TEntity ObterPorId(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public IEnumerable<TEntity> ObterTodosPaginado(int skip, int take)
        {
            return DbSet.Take(take).Skip(skip).ToList();
        }

        public IEnumerable<TEntity> BuscarVarios(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public TEntity BuscarUnico(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public PEntity ExecutarProcedure<PEntity>(string procedureName, SqlParameter[] parameters = null)
        {
            var dados = Db.Database.SqlQuery<PEntity>(ObterExecProcedure(procedureName, parameters), parameters).FirstOrDefault();
            return dados;
        }

        public IEnumerable<PEntity> ExecutarProcedureList<PEntity>(string procedureName, SqlParameter[] parameters = null)
        {
            var dados = Db.Database.SqlQuery<PEntity>(ObterExecProcedure(procedureName, parameters), parameters).ToList();
            return dados;
        }

        public void ExecutarProcedureNoQuery(string procedureName, SqlParameter[] parameters = null)
        {
            Db.Database.ExecuteSqlCommand(ObterExecProcedure(procedureName, parameters), parameters);
        }

        private string ObterNomesParemetros(SqlParameter[] parameters)
        {
            if (parameters != null)
            {
                var nomesList = new List<string>();

                foreach (var parametro in parameters)
                {
                    if (parametro.ParameterName.Contains("@"))
                    {
                        nomesList.Add(parametro.ParameterName);
                    }
                    else
                    {
                        nomesList.Add("@" + parametro.ParameterName);
                    }
                }

                if (nomesList.Count() > 0)
                {
                    string nomes = string.Join(",", nomesList.ToArray());
                    return nomes;
                }
            }

            return "";
        }

        private string ObterExecProcedure(string procedureName, SqlParameter[] parameters)
        {
            var exec = string.Format("EXEC {0} {1}",
                procedureName,
                ObterNomesParemetros(parameters));

            return exec;
        }

        protected object GetValueOrDBNull(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            if (string.IsNullOrEmpty(Convert.ToString(value)))
            {
                return DBNull.Value;
            }

            return value;
        }

        public bool SaveChanges()
        {
            return (Db.SaveChanges() > 0) ? true : false;
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}