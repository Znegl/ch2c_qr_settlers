using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class LogLine
    {
        public int LogLineID { get; set; }
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
    }
}
