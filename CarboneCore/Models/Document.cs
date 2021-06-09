using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carbone.Core.Models
{
        public class Document
        {
            public string type { get; set; }
            public byte[] data { get; set; }
            public string reportname { get; set; }
    }
}
