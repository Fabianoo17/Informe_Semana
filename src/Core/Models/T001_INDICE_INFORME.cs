using Core.Data;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class T001_INDICE_INFORME : Entity
    {

        [Key]
        public int T001_ID_INDICE_INFORME { get; set; }
        public string T001_DESC_INDICES { get; set; }


    }
}