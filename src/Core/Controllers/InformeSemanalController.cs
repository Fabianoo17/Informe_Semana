using Core.Data.Repositories;
using Core.ViewModels.InformeSemanal;
using System.Web.Mvc;

namespace Core.Controllers
{
    public class InformeSemanalController : Controller
    {
        private readonly RepositoryT003_COORDENACOES _coordenacaoes = new RepositoryT003_COORDENACOES();
        private readonly RepositoryT002_INFORME _informe = new RepositoryT002_INFORME();
        private readonly RepositoryT004_SEMANA _semana = new RepositoryT004_SEMANA();
        private readonly RepositoryVW001_INFORME_ATUAL _vwInforme = new RepositoryVW001_INFORME_ATUAL();

        ViewModelMontarInforme viewInforme = new ViewModelMontarInforme();

        public ActionResult Index()
        {


            viewInforme.t003_coordenacoes = _coordenacaoes.ObterTodos();
            viewInforme.t002_informe = _informe.ObterTodos();
            viewInforme.t004_semana = viewInforme.buscarSemana();
            viewInforme.vw001_informe_atual = _vwInforme.ObterTodos();
            

            return View(viewInforme);
        }

    }
}