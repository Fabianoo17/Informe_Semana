using Core.AutoMapper;
using Core.Logging.LoggerFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Core
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.RegisterMappings();

            Logger.SetLogPath();
        }

        protected void Application_Error()
        {
            var context = this.Context;
            var server = context.Server;
            var ex = server.GetLastError();
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
}
