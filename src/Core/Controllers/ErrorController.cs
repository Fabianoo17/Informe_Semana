using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Controllers
{
    public class ErrorController : Controller
    {
        [MvcSiteMapNode(Title = "Erro interno", Key = "Internal", ParentKey = "IndexHome")]
        public ActionResult Internal()
        {
            ViewBag.ErrorColor = "callout-info";
            ViewBag.ErrorTitle = "Ocorreu um erro!";
            ViewBag.ErrorMessage = "Ocorreu um erro interno, tente novamente ou contate um administrador.";
            return View("Error");
        }

        [MvcSiteMapNode(Title = "Não encontrado", Key = "NotFound", ParentKey = "IndexHome")]
        public ActionResult NotFound()
        {
            ViewBag.ErrorColor = "callout-warning";
            ViewBag.ErrorTitle = "Não encontrado!";
            ViewBag.ErrorMessage = "A página ou arquivo solicitado não existe ou não está disponível.";
            return View("Error");
        }

        [MvcSiteMapNode(Title = "Acesso Negado", Key = "AccessDanied", ParentKey = "IndexHome")]
        public ActionResult AccessDanied()
        {
            ViewBag.ErrorColor = "callout-danger";
            ViewBag.ErrorTitle = "Acesso negado!";
            ViewBag.ErrorMessage = "Você não possui perfil de acesso para o item requisitado.";
            return View("Error");
        }
    }
}