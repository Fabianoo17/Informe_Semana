using Core.Data.Repositories;
using Core.Filters;
using Core.Helpers;
using Core.Identity;
using Core.Identity.Models;
using Core.Models;
using Core.Service;
using Core.ViewModels;
using Core.ViewModels.Exemplo;
using MvcSiteMapProvider;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Core.Controllers
{
    [RoutePrefix("exemplos")]
    [AuthorizePerfil(new[] { Acesso.Administrador, Acesso.SemPerfil })]
    public class ExemploController : BaseController
    {
        private readonly ExemploRepository _exemploRepository;

        public ExemploController()
        {
            _exemploRepository = new ExemploRepository();
        }

        [HttpGet]
        [Route("listar")]
        [MvcSiteMapNode(Title = "Exemplos", Key = "IndexExemplo", ParentKey = "IndexHome")]
        public ActionResult Index()
        {
            var model = new ExemploListViewModel();
            ViewBag.Submit = 0;
            ViewBag.OpcoesList = new SelectList(OpcaoSimNao.Obter(), "Value", "Text");

            return View(model);
        }

        [HttpPost]
        [Route("listar")]
        public ActionResult Index(ExemploListViewModel model, int page = 1)
        {
            ViewBag.Submit = 1;
            ViewBag.OpcoesList = new SelectList(OpcaoSimNao.Obter(), "Value", "Text");

            int quatidadePorPagina = 100;

            model.ExemploList = _exemploRepository.ObterPorFiltro(model.Filtro, page, quatidadePorPagina);
            model.ItemCount = (model.ExemploList != null && model.ExemploList.Count() > 0) ? model.ExemploList.FirstOrDefault().TotalItens : 0;

            ViewBag.Pager = Pager.Items(model.ItemCount)
                .PerPage(quatidadePorPagina)
                .Move(page)
                .Segment(5)
                .Center();

            return View(model);
        }

        [HttpGet]
        [Route("listar-js")]
        [MvcSiteMapNode(Title = "Exemplos JS", Key = "Index2Exemplo", ParentKey = "IndexExemplo")]
        public ActionResult Index2()
        {
            var model = new ExemploListViewModel();
            ViewBag.Submit = 0;
            ViewBag.OpcoesList = new SelectList(OpcaoSimNao.Obter(), "Value", "Text");

            return View(model);
        }

        [HttpPost]
        [Route("pesquisar")]
        public ActionResult Search(ExemploListViewModel model, int page = 1)
        {
            int quatidadePorPagina = 100;

            model.ExemploList = _exemploRepository.ObterPorFiltro(model.Filtro, page, quatidadePorPagina);
            model.ItemCount = (model.ExemploList != null && model.ExemploList.Count() > 0) ? model.ExemploList.FirstOrDefault().TotalItens : 0;

            ViewBag.Pager = Pager.Items(model.ItemCount)
                .PerPage(quatidadePorPagina)
                .Move(page)
                .Segment(5)
                .Center();

            return PartialView("_SearchResult", model);
        }

        [HttpGet]
        [Route("detalhes/{id:int}")]
        public ActionResult Details(int id)
        {
            var model = _exemploRepository.ObterPorId(id);
            return PartialView("_Details", model);
        }

        [HttpGet]
        [Route("detalhes-linha/{id:int}")]
        public ActionResult DetailsLineTable(int id)
        {
            var model = _exemploRepository.ObterPorId(id);
            return PartialView("_DetailsLineTable", model);
        }

        [HttpGet]
        [Route("criar")]
        [AuthorizePerfil(new[] { Acesso.Administrador })]
        public ActionResult Create()
        {
            var model = new ExemploViewModel();
            return PartialView("_Create", model);
        }

        [HttpPost]
        [Route("criar")]
        [ValidateAntiForgeryToken]
        [AuthorizePerfil(new[] { Acesso.Administrador })]
        public ActionResult Create(ExemploEntity model)
        {
            var jsonResult = new JsonResultCustom();

            if (!ModelState.IsValid)
            {
                jsonResult.Messages = GetErrorsModelState();
                return Json(jsonResult);
            }

            _exemploRepository.Adicionar(model);
            if (_exemploRepository.SaveChanges())
            {
                jsonResult.Success = true;
                jsonResult.Messages.Add("Operação realizada com sucesso.");
                return Json(jsonResult);
            }

            jsonResult.Messages = GetErrorsModelState();
            return Json(jsonResult);
        }

        [HttpGet]
        [Route("editar/{id:int}")]
        [AuthorizePerfil(new[] { Acesso.Administrador })]
        public ActionResult Edit(int id)
        {
            var model = _exemploRepository.ObterPorId(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [Route("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        [AuthorizePerfil(new[] { Acesso.Administrador })]
        public ActionResult Edit(ExemploEntity model)
        {
            var jsonResult = new JsonResultCustom();

            if (!ModelState.IsValid)
            {
                jsonResult.Messages = GetErrorsModelState();
                return Json(jsonResult);
            }

            _exemploRepository.Atualizar(model);
            if (_exemploRepository.SaveChanges())
            {
                jsonResult.Success = true;
                jsonResult.Url = Url.Action("DetailsLineTable", new { id = model.EXEMPLO_ID });
                jsonResult.Messages.Add("Operação realizada com sucesso.");
                return Json(jsonResult);
            }

            jsonResult.Messages = GetErrorsModelState();
            return Json(jsonResult);
        }

        [HttpPost, FileDownload]
        [Route("exportar-excel")]
        public ActionResult ExportarExcel(ExemploListViewModel model)
        {
            var relatorio = _exemploRepository.ObterPorFiltro(model.Filtro);

            string excelName = string.Format("Relatório Teste {0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy-HH-mm"));

            var except = new List<string>();
            except.Add("TotalItens");

            byte[] filecontent = ExcelHelper.ExportEntity<ExemploViewModel>(relatorio, except);

            return File(filecontent, MediaTypeNames.Application.Octet, excelName);
        }

        [HttpPost, FileDownload]
        [Route("exportar-excel2")]
        public ActionResult ExportarExcel2(ExemploListViewModel model)
        {
            var relatorio = _exemploRepository.ObterPorFiltro(model.Filtro);

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Relatório Teste");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            var exModel = new ExemploViewModel();

            //Header of table
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            workSheet.Cells[1, 1].Value = exModel.DisplayNameFor(x => x.EXEMPLO_ID);
            workSheet.Cells[1, 2].Value = exModel.DisplayNameFor(x => x.NOME);
            workSheet.Cells[1, 3].Value = exModel.DisplayNameFor(x => x.DESCRICAO);
            workSheet.Cells[1, 4].Value = exModel.DisplayNameFor(x => x.ATIVO);

            //Size columns
            workSheet.Column(1).Width = 10;
            workSheet.Column(2).Width = 30;
            workSheet.Column(3).Width = 50;
            workSheet.Column(4).Width = 10;

            //Body of table 
            int recordIndex = 2;
            foreach (var item in relatorio)
            {
                workSheet.Cells[recordIndex, 1].Value = item.EXEMPLO_ID;
                workSheet.Cells[recordIndex, 2].Value = item.NOME;
                workSheet.Cells[recordIndex, 3].Value = item.DESCRICAO;
                workSheet.Cells[recordIndex, 4].Value = (item.ATIVO) ? "Ativo" : "Inativo";
                recordIndex++;
            }

            string excelName = string.Format("Relatório Teste {0}.xlsx", DateTime.Now.ToString("dd-MM-yyyy-HH-mm"));
            byte[] filecontent = excel.GetAsByteArray();

            return File(filecontent, MediaTypeNames.Application.Octet, excelName);
        }
    }
}