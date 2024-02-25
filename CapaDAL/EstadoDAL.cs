using CapaEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class EstadoDAL
    {
        
        public static async Task<int> CreateState(EstadoEN estadoEN)
        {
            using(var dbContext = new ContextDB())
            {
                dbContext.Add(estadoEN);
                return await dbContext.SaveChangesAsync();
            }
        }

        public static async Task<int> UpdateEstado(EstadoEN estadoEN)
        {
            using (var dbContext = new ContextDB())
            {
                var EstadoDB = await dbContext.state.FirstOrDefaultAsync(e => e.Id == estadoEN.Id);
                if (EstadoDB != null)
                {
                    EstadoDB.Nombre = EstadoDB.Nombre;
                    return await dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }

        public static async Task<int> DeleteEstado(EstadoEN estadoEN)
        {
            using (var dbContext = new ContextDB())
            {
                var EstadoDB = await dbContext.state.FirstOrDefaultAsync(e => e.Id == estadoEN.Id);
                if (EstadoDB != null)
                {
                    dbContext.Remove(EstadoDB);
                    return await dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }

        public static async Task<EstadoEN> GetEstadoAsync(EstadoEN estadoEN)
        {
            using (var dbContext = new ContextDB())
            {
                return await dbContext.state.FirstOrDefaultAsync(h => h.Id == estadoEN.Id);
            }
        }
    }
}
