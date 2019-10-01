using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Models
{
    public class OpcaoSimNao
    {
        public static IEnumerable<SelectListItem> Obter()
        {
            var dados = new List<SelectListItem>();
            dados.Add(new SelectListItem
            {
                Value = "0",
                Text = "Não"
            });
            dados.Add(new SelectListItem
            {
                Value = "1",
                Text = "Sim"
            });                      

            return dados;
        }
    }
}