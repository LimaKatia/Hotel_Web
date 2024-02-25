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

        public static async Task<int> DeleyeTipo(TipoHabitacionEN TipoEN)
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

        public static async Task<TipoHabitacionEN> GetTipoAsync(TipoHabitacionEN tipoEN)
        {
            using (var dbContext = new ContextDB())
            {
                return await dbContext.TipoDeHabitacion.FirstOrDefaultAsync(h => h.Id == tipoEN.Id);
            }
        }
    }
}
