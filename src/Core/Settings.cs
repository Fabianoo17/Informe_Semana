using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Core
{
    public static class Settings
    {
        public static string SystemName { get { return ConfigurationManager.AppSettings["SystemName"]; } }
    }
}