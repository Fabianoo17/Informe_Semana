using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class JsonResultCustom
    {
        public JsonResultCustom()
        {
            Success = false;
            Messages = new List<string>();
        }

        public bool Success { get; set; }

        public string Url { get; set; }

        public ICollection<string> Messages { get; set; }

        public object Data { get; set; }
    }
}
