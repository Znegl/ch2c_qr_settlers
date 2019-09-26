using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class TeamHasBought
    {
        public int TeamHasBoughtID { get; set; }
        public int TeamID { get; set; }
        public string Bought { get; set; }
        virtual public Team Team { get; set; }

    }
}
