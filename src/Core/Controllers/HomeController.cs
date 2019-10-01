using Core.Identity;
using Core.Identity.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Controllers
{
    [AuthorizePerfil(new[] { Acesso.Administrador, Acesso.SemPerfil })]
    public class HomeController : BaseController
    {
        [MvcSiteMapNode(Title = "Home", Key = "IndexHome")]
        public ActionResult Index()
        {
            return View();
        }
    }
}