using Core.Data;
using System.ComponentModel.DataAnnotations;
namespace Core.Models
{
    public class T005_PRORROGA_INFORME : Entity
    {
        [Key]
        public int T005_ID_PRORROGA_INFORME { get; set; }

        public int T002_ID_INFORME { get; set; }

        public int T004_NR_SEMANA { get; set; }

        public string T004_COMPETENCIA { get; set; }
    }
}