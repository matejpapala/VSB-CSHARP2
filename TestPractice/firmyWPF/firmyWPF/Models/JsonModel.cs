using System;
using System.Collections.Generic;
using System.Text;

namespace firmyWPF.Models
{
    public class JsonModel
    {
        public string obchodniJmeno { get; set; }
        public string dic { get; set; }
        public Sidlo sidlo { get; set; }
    }

    public class Sidlo
    {
        public string nazevObce { get; set; }
    }
}
