using CapaEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class ContextDB : DbContext
    {

        public DbSet<HabitacionEN> Habitacion { get; set; }
        public DbSet<EstadoEN> state { get; set; }
        public DbSet<TipoHabitacionEN> TipoDeHabitacion { get; set; }

        public DbSet<RoleEN> Role { get; set; }
        public DbSet<UserEN> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=Hotel_Agape;Integrated Security=True;Encrypt=false;TrustServerCertificate=True");
        }
    }
}