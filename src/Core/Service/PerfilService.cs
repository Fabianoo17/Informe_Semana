using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Service
{
    public class PerfilService
    {
        public IEnumerable<PerfilViewModel> ObterPerfis()
        {
            var db = new Entities();
            var dados = db.T002_PERFIL.Select(x =>
                new PerfilViewModel
                {
                    Ativo = x.ATIVO,
                    Id = x.PERFIL_ID,
                    Nome = x.NOME
                }).ToList();

            return dados;
        }
    }
}