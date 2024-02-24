using CapaEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class ContextDB : DbContext
    {

        public DbSet<HabitacionEN> Habitacion { get; set; }

        //public DbSet<SalonEN> Salon { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=Hotel_Agape;Integrated Security=True; Encrypt = false;
                TrustServerCertificate = True");
        }
    }
}
