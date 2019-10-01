using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Identity.Models
{
    [Table("VW001_USUARIO_COORDENACAO")]
    public class Usuario
    {
        [Key]
        public string MATRICULA { get; set; }

        public string NOME { get; set; }

        public string SG_UNID_ADM { get; set; }

        public int NU_UNID_ADM { get; set; }

        public int T003_ID_COORDENACAO { get; set; }

        public string T003_SG_COORDENACAO { get; set; }     

        [NotMapped]
        public string TIPO { get; set; }
    }
}
