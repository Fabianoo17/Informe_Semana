using Core.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class T004_SEMANA : Entity
    {
        [Key]
        public int T004_ID_SEMANA { get; set; }
        public int T004_NR_SEMANA_MES { get; set; }
        public string T004_DESC_SEMANA { get; set; }
        public DateTime T004_DT_INICIO { get; set; }
        public DateTime T004_DT_FIM { get; set; }
        public string T004_COMPETENCIA { get; set; }
    }
}