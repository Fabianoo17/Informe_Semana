using Core.Identity;
using Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Core.Helpers
{
    public static class AuthorizePerfilHelper
    {
        public static bool IfAccess(this WebViewPage page, Acesso[] perfisRequisitados)
        {
            return AuthorizeBase.ValidatePermission(perfisRequisitados);
        }
    }
}