using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Controllers
{
    public class BaseController : Controller
    {
        protected ICollection<string> GetErrorsModelState()
        {
            var models = new List<string>();

            foreach (var state in ModelState.Values)
            {
                foreach (var erro in state.Errors)
                {
                    models.Add(erro.ErrorMessage);
                }
            }

            return models;
        }
    }
}