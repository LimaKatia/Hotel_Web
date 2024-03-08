using CapaEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class TipoHabitacionDAL
    {

        public static async Task<int> CreateTipo(TipoHabitacionEN tipoEN)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(tipoEN);
                return await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> UpdateTipo(TipoHabitacionEN tipoEN)
        {
            using (var dbContext = new ContextDB())
            {
                var TipoDB = await dbContext.TipoDeHabitacion.FirstOrDefaultAsync(e => e.Id == tipoEN.Id);
                if (TipoDB != null)
                {
                    TipoDB.Nombre = TipoDB.Nombre;
                    return await dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }

        public static async Task<int> DeleteTipo(TipoHabitacionEN TipoEN)
        {
            using (var dbContext = new ContextDB())
            {
                var TipoDB = await dbContext.TipoDeHabitacion.FirstOrDefaultAsync(e => e.Id == TipoEN.Id);
                if (TipoDB != null)
                {
                    dbContext.Remove(TipoDB);
                    return await dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }

        public static async Task<TipoHabitacionEN> GetByIdAsync(TipoHabitacionEN tipo)
        {
            var tipoDB = new TipoHabitacionEN();
            using (var dbContext = new ContextDB())
            {
                tipoDB = await dbContext.TipoDeHabitacion.FirstOrDefaultAsync(t => t.Id == tipo.Id);

            }
            return tipoDB;
        }

        public static async Task<List<TipoHabitacionEN>> GetAllAsync()
        {
            var habitaciones = new List<TipoHabitacionEN>();
            using (var dbContext = new ContextDB())
            {
                habitaciones = await dbContext.TipoDeHabitacion.ToListAsync();
            }
            return habitaciones;
        }

        internal static IQueryable<TipoHabitacionEN> QuerySelect(IQueryable<TipoHabitacionEN> query, TipoHabitacionEN tipo)
        {
            if (tipo.Id > 0)
                query = query.Where(r => r.Id == tipo.Id);

            if (!string.IsNullOrWhiteSpace(tipo.Nombre))
                query = query.Where(r => r.Nombre.Contains(tipo.Nombre));

            query = query.OrderByDescending(r => r.Id).AsQueryable();

            if (tipo.Top_Aux > 0)
                query = query.Take(tipo.Top_Aux).AsQueryable();

            return query;
        }

        public static async Task<List<TipoHabitacionEN>> SearchAsync(TipoHabitacionEN tipo)
        {
            var tipos = new List<TipoHabitacionEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.TipoDeHabitacion.AsQueryable();
                select = QuerySelect(select, tipo);
                tipos = await select.ToListAsync();
            }
            return tipos;
        }
    }
}
