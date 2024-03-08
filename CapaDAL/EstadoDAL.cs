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
        
        ////////////////CREAR////////////
        public static async Task<int> CreateState(EstadoEN estadoEN)
        {
            int result = 0;
            using(var dbContext = new ContextDB())
            {
                dbContext.Add(estadoEN);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }

        ///////////Actualizar////////////
        public static async Task<int> UpdateEstado(EstadoEN estadoEN)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var EstadoDB = await dbContext.state.FirstOrDefaultAsync(e => e.Id == estadoEN.Id);
                if (EstadoDB != null)
                {
                    EstadoDB.Nombre = EstadoDB.Nombre;
                    dbContext.Update(EstadoDB);
                    return await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        /////////ELIMINAR//////////////
        public static async Task<int> DeleteEstado(EstadoEN estadoEN)
        {
            int result = 0;

            using (var dbContext = new ContextDB())
            {
                var EstadoDB = await dbContext.state.FirstOrDefaultAsync(e => e.Id == estadoEN.Id);

                if (EstadoDB != null)
                {
                    dbContext.Remove(EstadoDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<EstadoEN> GetEstadoAsync(EstadoEN estadoEN)
        {
            var estadoDB = new EstadoEN();
            using (var dbContext = new ContextDB())
            {
                estadoDB = await dbContext.state.FirstOrDefaultAsync(h => h.Id == estadoEN.Id);
            }
            return estadoDB;
        }

        public static async Task<List<EstadoEN>> GetAllAsync()
        {
            var estado = new List<EstadoEN>();
            using (var dbContext = new ContextDB())
            {
                estado = await dbContext.state.ToListAsync();
            }
            return estado;
        }

        internal static IQueryable<EstadoEN> QuerySelect(IQueryable<EstadoEN> query, EstadoEN estado)
        {
            if (estado.Id > 0)
            {
                query = query.Where(c => c.Id == estado.Id);
            }

            if (!string.IsNullOrEmpty(estado.Nombre.ToString()))
            {
                query = query.OrderByDescending(c => c.Id);
            }

            return query;
        }

        public static async Task<List<EstadoEN>> SearchAsync(EstadoEN estado)
        {
            var EstadoDB = new List<EstadoEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.state.AsQueryable();
                select = QuerySelect(select, estado);
                EstadoDB = await select.ToListAsync();
            }
            return EstadoDB;
        }
    }
}
