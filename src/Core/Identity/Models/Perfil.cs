using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Identity.Models
{
    public class Perfil
    {
        [Key]
        public int PERFIL_ID { get; set; }

        public string NOME { get; set; }

        public bool ATIVO { get; set; }
    }
}
