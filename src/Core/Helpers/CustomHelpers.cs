using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Helpers
{
    public static class CustomHelpers
    {
        public static string IsChecked(this HtmlHelper html, bool isCheched)
        {
            return isCheched ? "checked" : "";
        } 
    }
}