using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Core.Identity.Models
{
    public class ControleAcesso
    {
        [Key]
        public int PerfilId { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public string AtualizadoPor { get; set; }

        public DateTime? DataAtualizacao { get; set; }
    }
}