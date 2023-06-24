using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butler.Model.Request.Base
{
    public class Response
    {
        public bool Success { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}
