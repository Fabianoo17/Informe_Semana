using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.ViewModels.Exemplo
{
    public class FiltroExemploViewModel
    {
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public int? Ativo { get; set; }
    }
}