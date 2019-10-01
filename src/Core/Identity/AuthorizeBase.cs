using Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Identity
{
    public static class AuthorizeBase
    {
        public static bool ValidatePermission(Acesso[] perfisRequisitados)
        {
            bool authorized = false;
            var listaPerfisUsuario = UsuarioLogado.Perfis;

            foreach (var perfilReq in perfisRequisitados)
            {
                if (listaPerfisUsuario.Contains(perfilReq))
                {
                    authorized = true;
                    break;
                }
            }

            return authorized;
        }
    }
}
