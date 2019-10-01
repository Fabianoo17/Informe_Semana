using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.ViewModels.Exemplo
{
    public class ExemploListViewModel
    {
        public ExemploListViewModel()
        {
            ExemploList = new List<ExemploViewModel>();
        }

        public FiltroExemploViewModel Filtro { get; set; }

        public ExemploViewModel Exemplo { get; set; }
        public IEnumerable<ExemploViewModel> ExemploList { get; set; }

        public int ItemCount { get; set; }
    }
}