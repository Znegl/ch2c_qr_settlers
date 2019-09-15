using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class Ch2CContext : DbContext
    {
        //public Ch2CContext()
        //{

        //}
        public Ch2CContext(DbContextOptions<Ch2CContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<LogLine> Logs { get; set; }
        public DbSet<ResourceRead> ResourceReads { get; set; }
    }
}
