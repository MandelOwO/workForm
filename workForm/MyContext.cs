using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workForm
{
    public class MyContext : DbContext
    {
        public DbSet<Tables.User> tbUsers { get; set; }
        public DbSet<Tables.Customer> tbCustomers { get; set; }
        public DbSet<Tables.Project> tbProjects { get; set; }
        public DbSet<Tables.Work> tbWorks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database= 3a1_dvorakmichal_db2;user=dvorakmichal;password=123456;SslMode=none");
        }

    }

}
