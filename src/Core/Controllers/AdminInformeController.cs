using Core.Data.Context;
using Core.Data.Repositories;
using Core.Identity;
using Core.Identity.Models;
using Core.Models;
using Core.ViewModels;
using Core.ViewModels.InformeSemanal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Controllers
{
    public class AdminInformeController : Controller
    {
        ViewModelMontarInforme modelInforme = new ViewModelMontarInforme();
        RepositoryT002_INFORME _informe = new RepositoryT002_INFORME();
        RepositoryT003_COORDENACOES _dbcoordenacao = new RepositoryT003_COORDENACOES();
        // GET: AdminInforme
        [AuthorizePerfil(new[] { Acesso.Administrador, Acesso.Basico })]
        public ActionResult Index()
        {
            var coord = _dbcoordenacao.ObterTodos();
            var mat = UsuarioLogado.Perfis;

            if (mat.Contains(Acesso.Basico))
            {
                coord = _dbcoordenacao.ObterTodos().Where(c => c.T003_ID_COORDENACAO == UsuarioLogado.idCoordencao);
            }

            ViewBag.T003_ID_COORDENACAO = new SelectList(coord, "T003_ID_COORDENACAO", "T003_SG_COORDENACAO");
            modelInforme.t002_informe = _informe.ObterTodos();


            return View(modelInforme);
        }

        [HttpPost]
        [AuthorizePerfil(new[] { Acesso.Administrador, Acesso.Basico })]
        public ActionResult Create([Bind(Include = "T002_ID_INFORME,T002_INFORME_TITULO,T002_RESULTADO_ESPERADO,T001_ID_INDICE_INFORME,T003_ID_COORDENACAO,T004_NR_SEMANA,T002_IN_REUSADO,T002_MAT_CRIADO_POR,T002_DT_ATUALIZACAO,T002_COMPETENCIA")] T002_INFORME t002_INFORME)
        {
            JsonResultCustom jsonResult = new JsonResultCustom();
            t002_INFORME.T002_DT_ATUALIZACAO = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (t002_INFORME.T002_ID_INFORME > 0)
                {
                    _informe.Atualizar(t002_INFORME);
                    _informe.SaveChanges();
                    jsonResult.Success = true;
                    jsonResult.Messages.Add("Alteração realizada com sucesso.");
                }
                else
                {
                    _informe.Adicionar(t002_INFORME);
                    _informe.SaveChanges();
                    jsonResult.Success = true;
                    jsonResult.Messages.Add("Informação registada com sucesso.");
                }
            }
            return Json(jsonResult);
        }


        [HttpPost]
        [AuthorizePerfil(new[] { Acesso.Administrador, Acesso.Basico })]
        public ActionResult SalvaProrrogacao([Bind(Include = "T005_ID_PRORROGA_INFORME,T002_ID_INFORME,T004_NR_SEMANA,T004_COMPETENCIA")] T005_PRORROGA_INFORME t005_PRORROGA_INFORME)
        {
            JsonResultCustom jsonResult = new JsonResultCustom();
    
            if (ModelState.IsValid)
            {
                //if (t002_INFORME.T002_ID_INFORME > 0)
                //{
                //    _informe.Atualizar(t002_INFORME);
                //    _informe.SaveChanges();
                //    jsonResult.Success = true;
                //    jsonResult.Messages.Add("Alteração realizada com sucesso.");
                //}
  
            }
            return Json(jsonResult);
        }



        [AuthorizePerfil(new[] { Acesso.Administrador, Acesso.Basico })]
        public ActionResult carregaForm(int? COORDENACAO)
        {
            IEnumerable<VW001_INFORME_ATUAL> viewInforme = new List<VW001_INFORME_ATUAL>();
            IEnumerable<T002_INFORME> informe = new List<T002_INFORME>();
            RepositoryT002_INFORME _dbInforme = new RepositoryT002_INFORME();
            RepositoryVW001_INFORME_ATUAL _dbVwInforme = new RepositoryVW001_INFORME_ATUAL();
            RepositoryT004_SEMANA _semana = new RepositoryT004_SEMANA();
            string competencia = _semana.ObterTodos().OrderByDescending(x => x.T004_ID_SEMANA).FirstOrDefault().T004_COMPETENCIA;
            var semana = _semana.ObterTodos().Where(x => x.T004_DT_FIM >= DateTime.Now.Date).FirstOrDefault().T004_NR_SEMANA_MES;
            ViewBag.coord = COORDENACAO;
            ViewBag.Mat = "DESKTOP-FABIA";
            ViewBag.comp = competencia;
            ViewBag.semana = semana;

            informe = _dbInforme.ObterTodos();
            informe = informe.Where(i => i.T004_NR_SEMANA == semana && i.T003_ID_COORDENACAO == COORDENACAO && i.T002_COMPETENCIA == competencia).Take(2);

            viewInforme = _dbVwInforme.ObterTodos().Where(i => i.T003_ID_COORDENACAO == COORDENACAO).OrderByDescending(i => i.T002_ID_INFORME).Take(2);


            return View("_carregaForm", viewInforme);
        }
    }
}