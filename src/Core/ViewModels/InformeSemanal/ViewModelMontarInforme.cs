using Core.Data.Repositories;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.ViewModels.InformeSemanal
{
    public class ViewModelMontarInforme
    {
        public IEnumerable<T003_COORDENACOES> t003_coordenacoes { get; set; }
        public IEnumerable<T002_INFORME> t002_informe { get; set; }
        public IEnumerable<VW001_INFORME_ATUAL> vw001_informe_atual { get; set; }
        public T004_SEMANA t004_semana { get; set; }

        public ViewModelMontarInforme()
        {
            t003_coordenacoes = new List<T003_COORDENACOES>();
            t002_informe = new List<T002_INFORME>();
            vw001_informe_atual = new List<VW001_INFORME_ATUAL>();
            t004_semana = new T004_SEMANA();
        }

        private readonly RepositoryT002_INFORME _informe = new RepositoryT002_INFORME();
 
                                   

        public IEnumerable<T002_INFORME> buscarInformes(int id, int indice) {

            t002_informe = _informe.ObterTodos().Where(x => x.T003_ID_COORDENACAO == id && x.T001_ID_INDICE_INFORME == indice);

            return t002_informe;
        }
        public T004_SEMANA buscarSemana()
        {
            RepositoryT004_SEMANA _semana = new RepositoryT004_SEMANA();


            return _semana.ObterTodos().Where(x => x.T004_DT_INICIO <= DateTime.Now.Date && x.T004_DT_FIM >= DateTime.Now.Date).FirstOrDefault();

        }
       
    }
}