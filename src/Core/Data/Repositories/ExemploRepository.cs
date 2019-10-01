using Core.Data.Context;
using Core.ViewModels.Exemplo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Core.Data.Repositories
{
    public class ExemploRepository : Repository<ExemploEntity>
    {
        public ExemploRepository()
            : base(new DefaultDbContext())
        {
        }        

        public IEnumerable<ExemploViewModel> ObterPorFiltro(FiltroExemploViewModel filtro, int pagina = 0, int quatidadePorPagina = 100)
        {
            SqlParameter[] parametros = {
                new SqlParameter{ 
                    ParameterName = "@Nome",
                    Value = GetValueOrDBNull(filtro.Nome) 
                },
                new SqlParameter{ 
                    ParameterName = "@Descricao",
                    Value = GetValueOrDBNull(filtro.Descricao)  
                },
                new SqlParameter{ 
                    ParameterName = "@Ativo", 
                    Value = GetValueOrDBNull(filtro.Ativo)
                },
                new SqlParameter{ 
                    ParameterName = "@Pagina", 
                    Value = pagina
                },
                new SqlParameter{ 
                    ParameterName = "@QtdPorPagina",
                    Value = quatidadePorPagina
                }
            };

            var dados = ExecutarProcedureList<ExemploViewModel>("[dbo].[spTestePaginacao]", parametros);

            return dados;
        }


    }
}