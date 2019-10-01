using Core.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class T002_INFORME : Entity
    {
        [Key]
        public int T002_ID_INFORME { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string T002_INFORME_TITULO { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string T002_RESULTADO_ESPERADO { get; set; }
        public int T001_ID_INDICE_INFORME { get; set; }
        public int T003_ID_COORDENACAO { get; set; }
        public int T004_NR_SEMANA { get; set; }
        public bool? T002_IN_REUSADO { get; set; }
        public string T002_MAT_CRIADO_POR { get; set; }
        public DateTime T002_DT_ATUALIZACAO { get; set; }
        public string T002_COMPETENCIA { get; set; }




    }
}