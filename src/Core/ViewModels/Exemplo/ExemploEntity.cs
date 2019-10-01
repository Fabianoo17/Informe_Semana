using Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Core.ViewModels.Exemplo
{
    [Table("T004_EXEMPLO")]
    public class ExemploEntity : Entity, IValidatableObject
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

        //public HttpPostedFileBase Foto { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ATIVO && string.IsNullOrEmpty(NOME))
            {
                yield return
                    new ValidationResult(errorMessage: "Se o Status ativo, o campo Nome é obrigatório.",
                        memberNames: new[] { "NOME" });
            }
        }
    }
}