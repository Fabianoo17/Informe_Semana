using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Core.ViewModels.Exemplo
{
    public class ExemploViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public int EXEMPLO_ID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string NOME { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Descrição")]
        public string DESCRICAO { get; set; }

        public bool ATIVO { get; set; }

        public HttpPostedFileBase Foto { get; set; }

        internal int TotalItens { get; set; }
    }
}