using System;
using System.Web.Script.Serialization;

namespace Core.Logging.Data
{
    public class AuditoriaModel
    {
        public AuditoriaModel(string user, string ip, string browser, string browserVersion, string action, string model = null)
        {
            User = user;
            IP = ip;
            Browser = browser;
            BrowserVersion = browserVersion;
            Action = action;
            Model = model;
            LogId = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public Guid LogId { get; set; }
        public DateTime Date { get; private set; }
        public string User { get; private set; }
        public string IP { get; private set; }
        public string Browser { get; private set; }
        public string BrowserVersion { get; private set; }
        public string Action { get; private set; }
        public string Model { get; private set; }

        public string ModelToJson(object model)
        {
            return new JavaScriptSerializer().Serialize(model);
        }
    }
}