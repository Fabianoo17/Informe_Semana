using Core.Identity.Models;
using Core.Identity.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Core.Identity
{
    public static class UsuarioLogado
    {
        private static string _nome { get; set; }
        private static string _unidade { get; set; }
        private static int _idCoordencao { get; set; }
        private static string _coordencao { get; set; }

        public static string Matricula
        {
            get { return RetornaMatricula(); }
        }

        public static string Nome
        {
            get
            {
                AtualizaDadosUsuario();
                return _nome;
            }
        }

        public static string Unidade
        {
            get
            {
                AtualizaDadosUsuario();
                return _unidade;
            }
        }

        public static int idCoordencao
        {
            get
            {
                AtualizaDadosUsuario();
                return _idCoordencao;
            }
        }

        public static string Coordencao
        {
            get
            {
                AtualizaDadosUsuario();
                return _coordencao;
            }
        }

        public static IEnumerable<Acesso> Perfis
        {
            get { return RetornaPerfis(); }
        }

        private static string RetornaMatricula()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string matriculaCompleta = HttpContext.Current.User.Identity.Name;
                string expressao = @"\w+\\";
                Regex rgx = new Regex(expressao);
                return rgx.Replace(matriculaCompleta, "").ToUpper();
            }

            return Environment.UserName.ToUpper();
        }

        private static void AtualizaDadosUsuario()
        {
            if (string.IsNullOrEmpty(_nome))
            {
                var usuarioService = new UsuarioIdentityRepository();
                var usuario = usuarioService.ObterUsuarioPorMatricula(Matricula);

                if (usuario != null)
                {
                    _nome = usuario.NOME;
                    _unidade = usuario.SG_UNID_ADM;
                    _idCoordencao = usuario.T003_ID_COORDENACAO;
                    _coordencao = usuario.T003_SG_COORDENACAO;
                }
                else
                {
                    _nome = Matricula;
                }
            }
        }

        private static IEnumerable<Acesso> RetornaPerfis()
        {
            var usuarioService = new UsuarioIdentityRepository();
            var perfisUsuario = usuarioService.ObterPerfils(Matricula);

            var perfis = new List<Acesso>();
            perfis.Add(Acesso.SemPerfil);

            foreach (var perfil in perfisUsuario)
            {
                perfis.Add((Acesso)perfil.PERFIL_ID);
            }

            return perfis;
        }
    }
}
