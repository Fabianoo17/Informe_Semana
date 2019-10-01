using Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Core.Identity
{
    public class AuthorizePerfilAttribute : AuthorizeAttribute
    {
        private readonly Acesso[] _perfisRequisitados;

        public AuthorizePerfilAttribute(Acesso[] perfisRequisitados)
        {
            _perfisRequisitados = perfisRequisitados;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthorizeBase.ValidatePermission(_perfisRequisitados);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
