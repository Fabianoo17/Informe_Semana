using AutoMapper;
using Core.Identity;
using Core.Identity.Models;
using Core.Identity.Service;
using Core.Service;
using Core.ViewModels;
using Core.ViewModels.GestaoAcesso;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Controllers
{
    [AuthorizePerfil(new[] { Acesso.Administrador })]
    public class GestaoAcessoController : BaseController
    {
        private readonly UsuarioIdentityRepository _usuarioIdentityRepository;

        public GestaoAcessoController()
        {
            _usuarioIdentityRepository = new UsuarioIdentityRepository();
        }

        [MvcSiteMapNode(Title = "Gestão de Acessos", Key = "IndexGestaoAcesso", ParentKey = "IndexHome")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObterUsuario(FiltroViewModel usuarioModel)
        {
            JsonResultCustom jsonResult = new JsonResultCustom();

            if (!ModelState.IsValid)
            {
                jsonResult.Messages = GetErrorsModelState();
                return Json(jsonResult);
            }

            var usuario = _usuarioIdentityRepository.ObterUsuarioPorMatricula(usuarioModel.MatriculaUsuario);

            if (usuario != null)
            {
                jsonResult.Data = usuario;
                jsonResult.Success = true;
                jsonResult.Messages.Add("Usuário localizado com sucesso.");
            }
            else
            {
                jsonResult.Success = false;
                jsonResult.Messages.Add("o usuário informado não foi localizado.");
            }            

            return Json(jsonResult);
        }

        [HttpPost]
        public ActionResult ObterAcesso(FiltroViewModel usuarioModel)
        {
            ViewBag.MatriculaPerfil = usuarioModel.MatriculaUsuario.ToUpper();

            if (ModelState.IsValid)
            {
                var perfis = _usuarioIdentityRepository.ObterAcesso(usuarioModel.MatriculaUsuario);
                var perfisViewModel = Mapper.Map<IEnumerable<ControleAcessoViewModel>>(perfis);

                return PartialView("_ListPerfil", perfisViewModel);
            }

            return PartialView("_ListPerfil", null);
        }

        [HttpPut]
        public ActionResult AtualizarAcesso(string matricula, int perfilId, bool ativo)
        {
            JsonResultCustom jsonResult = new JsonResultCustom();

            var alterado = _usuarioIdentityRepository.AtualizarPerfil(matricula.ToUpper(), perfilId, ativo);
            if (alterado)
            {
                jsonResult.Success = true;
                jsonResult.Messages.Add("Alteração realizada com sucesso.");
            }

            return Json(jsonResult);
        }
    }
}