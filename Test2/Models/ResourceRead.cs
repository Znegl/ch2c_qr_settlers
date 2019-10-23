using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class ResourceRead
    {
        public int ResourceReadID { get; set; }
        public int TeamID { get; set; }
        public string ResourceUUID { get; set; }
        public DateTime ReadAt { get; set; }
    }
}
