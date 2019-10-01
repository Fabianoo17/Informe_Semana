using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Core.Helpers
{
    public static class UrlHelpers
    {      
        public static string IsActionActive(this HtmlHelper html, string action, string control, bool controlOnly = false)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = control.ToLower() == routeControl.ToLower() && action.ToLower() == routeAction.ToLower();

            if (controlOnly)
            {
                returnActive = control.ToLower() == routeControl.ToLower();
            }

            return returnActive ? "active" : "";
        }

        public static string IsActionActiveTreeview(this HtmlHelper html, string control)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            var returnActive = control.ToLower() == routeControl.ToLower();

            return returnActive ? "active" : "";
        }        
    }
}