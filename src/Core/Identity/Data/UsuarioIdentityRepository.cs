using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Identity.Models;
using Core.Identity.Data;
using System.Data.SqlClient;

namespace Core.Identity.Service
{
    public class UsuarioIdentityRepository : IDisposable
    {
        private readonly OLAP001DbContext _OLAP001DbContext;
        private readonly IdentityDbContext _identityDbContext;

        public UsuarioIdentityRepository()
        {
            _OLAP001DbContext = new OLAP001DbContext();
            _identityDbContext = new IdentityDbContext();
        }

        public IEnumerable<Perfil> ObterPerfils(string matricula)
        {
            SqlParameter[] parametros = {
                new SqlParameter{ 
                    ParameterName = "@Matricula", 
                    Value = matricula
                }
            };

            var dados = _identityDbContext.Database
                .SqlQuery<Perfil>("EXEC [dbo].[spListarPerfisAtivosUsuario] @Matricula", parametros)
                .ToList();

            return dados;
        }

        public IEnumerable<ControleAcesso> ObterAcesso(string matricula)
        {
            SqlParameter[] parametros = {
                new SqlParameter{ 
                    ParameterName = "@Matricula", 
                    Value = matricula
                }
            };

            var dados = _identityDbContext.Database
                .SqlQuery<ControleAcesso>("EXEC [dbo].[spListarPerfisAcesso] @Matricula", parametros)
                .ToList();

            return dados;
        }

        public Usuario ObterUsuarioPorMatricula(string matricula)
        {
            var dados = _OLAP001DbContext.Usuarios
                .Where(x => x.MATRICULA == matricula)
                .FirstOrDefault();

            return dados;
        }

        public bool AtualizarPerfil(string matricula, int perfilId, bool ativo)
        {
            SqlParameter[] parametros = {
                new SqlParameter{ 
                    ParameterName = "@Matricula", 
                    Value = matricula
                },
                new SqlParameter{ 
                    ParameterName = "@PerfilId", 
                    Value = perfilId
                },
                new SqlParameter{ 
                    ParameterName = "@Ativo", 
                    Value = ativo
                },
                new SqlParameter{ 
                    ParameterName = "@AtualizadoPor", 
                    Value = UsuarioLogado.Matricula
                }
            };

            _identityDbContext.Database.ExecuteSqlCommand("EXEC [dbo].[spAtualizarPerfil] @Matricula, @PerfilId, @Ativo, @AtualizadoPor", parametros);

            return true;
        }

        public void Dispose()
        {
            _identityDbContext.Dispose();
            _OLAP001DbContext.Dispose();
        }
    }
}