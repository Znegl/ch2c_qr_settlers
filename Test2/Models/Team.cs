using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class Team
    {
        public string TeamName { get; set; }
        public int TeamID { get; set; }
        public int Metal { get; set; }
        public int Wood { get; set; }
        public int Cloth { get; set; }
        public int Plastic { get; set; }
        public DateTime StarTtime { get; set; }
        public int NumberOfActions { get; set; }
    }
}
