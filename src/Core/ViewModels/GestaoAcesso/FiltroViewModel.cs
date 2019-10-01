using Core.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Core.ViewModels.GestaoAcesso
{
    public class FiltroViewModel
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        //[Matricula]
        public string MatriculaUsuario { get; set; }
    }
}