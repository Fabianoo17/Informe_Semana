using Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Core.Filters
{
    public class GlobalFilterTool : ActionFilterAttribute
    {
        private readonly LogAuditoriaHelper _logAuditoria;

        public GlobalFilterTool()
        {
            _logAuditoria = new LogAuditoriaHelper();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logAuditoria.RegistrarLog(filterContext);

            if (filterContext.Exception != null)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
