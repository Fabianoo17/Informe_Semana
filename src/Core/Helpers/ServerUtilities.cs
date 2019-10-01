using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Helpers
{
    public class ServerUtilities
    {
        public static bool IsLocalhost()
        {
            var host = HttpContext.Current.Request.Url.Host;

            if (host.Contains("localhost"))
            {
                return true;
            }
            return false;
        }

        public static bool IsDev()
        {
            var host = HttpContext.Current.Request.Url.Host;

            if (host.Contains("localhost")
                || host.Contains(".hom.")
                || host.Contains(".hmp.")
                || host.Contains(".des."))
            {
                return true;
            }
            return false;
        }
    }
}
