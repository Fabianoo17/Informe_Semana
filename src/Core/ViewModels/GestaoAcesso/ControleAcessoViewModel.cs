using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.ViewModels.GestaoAcesso
{
    public class ControleAcessoViewModel
    {
        public int PerfilId { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "Atualizado por")]
        public string AtualizadoPor { get; set; }

        [Display(Name = "Data atualização")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? DataAtualizacao { get; set; }
    }
}