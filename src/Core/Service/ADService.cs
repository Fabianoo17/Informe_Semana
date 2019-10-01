using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace Core.Service
{
    public class ADService
    {
        private readonly string _domain = "corpcaixa";
        private readonly string _user = "s7562226";
        private readonly string _password = "wLawrLa5";

        public bool ValidaMatricula(string matricula)
        {
            PrincipalContext context = new PrincipalContext(ContextType.Domain, _domain, _user, _password);
            UserPrincipal user = UserPrincipal.FindByIdentity(context, matricula);

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}