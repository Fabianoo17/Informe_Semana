using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Core.Logging.LoggerFile;
using Core.Identity;
using Core.Logging.Data;

namespace Core.Logging
{
    public class LogAuditoriaHelper : IDisposable
    {
        private readonly LogginDbContext _context;
        private Dictionary<string, string> Item = new Dictionary<string, string>();

        public LogAuditoriaHelper()
        {
            _context = new LogginDbContext();
        }

        public void RegistrarLog(ActionExecutedContext filterContext)
        {
            try
            {
                var modelJson = "";
                if (filterContext.HttpContext.Request.HttpMethod.ToLower() == "post")
                {
                    var form = Form(filterContext.HttpContext);
                    var token = form.FirstOrDefault(c => c.Key == "__RequestVerificationToken");

                    if (token != null)
                    {
                        form.Remove(form.FirstOrDefault(c => c.Key == "__RequestVerificationToken"));
                    }

                    modelJson = form.Aggregate("{", (current, item) => current + string.Format("'{0}':" + "'{1}',", item.Key, item.Value)) + "}";
                }

                var log = new AuditoriaModel(
                    UsuarioLogado.Matricula,
                    GetIP(filterContext),
                    filterContext.HttpContext.Request.Browser.Browser,
                    filterContext.HttpContext.Request.Browser.Version,
                    filterContext.HttpContext.Request.Url.AbsoluteUri,
                    modelJson);

                _context.LogAuditoria.Add(log);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Gravar Log de Erro
                Logger.SetLogPath();

                var context = HttpContext.Current;
                var statusCode = new HttpException(null, ex).GetHttpCode();

                var sb = new StringBuilder();

                sb.AppendLine(statusCode + ", " + context.Request.HttpMethod + ", " + context.Request.Url.ToString());

                if (statusCode == 500)
                {
                    sb.AppendLine(ex.ToString());
                    Logger.Error(sb.ToString());
                }
                else
                {
                    Logger.Warning(sb.ToString());
                }
            }
        }

        public IEnumerable<AuditoriaModel> ObterLogs()
        {
            return _context.LogAuditoria.OrderByDescending(c => c.Date).ToList();
        }

        public IEnumerable<AuditoriaModel> Buscar(Expression<Func<AuditoriaModel, bool>> predicate)
        {
            return _context.LogAuditoria.Where(predicate);
        }

        private static List<Item> Form(HttpContextBase httpContext)
        {
            return httpContext.Request.Form.Keys.OfType<string>().Select(k => new Item(k, httpContext.Request.Form[k])).ToList();
        }

        private string GetIP(ActionExecutedContext filterContext)
        {
            return filterContext.HttpContext.Request.ServerVariables["SERVER_NAME"] == "localhost" ? "Acesso Local" : filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"];
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class Item
    {
        public Item(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}