using CapaEN;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class HabitacionDAL
    {


        public static async Task<int> CreateHabitacion(HabitacionEN habitacion)
        {
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(habitacion);
                return await dbContext.SaveChangesAsync();
            }
        }

        public static async Task<int> UpdateHabitacion(HabitacionEN habitacion)
        {
            using (var dbContext = new ContextDB())
            {
                var HabitacionDB = await dbContext.Habitacion.FirstOrDefaultAsync(h => h.Id == habitacion.Id);
                if (HabitacionDB != null)
                {
                    HabitacionDB.NumeroDeHabitacion = habitacion.NumeroDeHabitacion;
                    HabitacionDB.Precio = habitacion.Precio;
                    HabitacionDB.IdEstado = habitacion.IdEstado;
                    HabitacionDB.IdTipoDeHabitacion = habitacion.IdTipoDeHabitacion;
                    return await dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }

        public static async Task<int> DeleteHabitacion(HabitacionEN habitacion)
        {
            using (var dbContext = new ContextDB())
            {
                var HabitacionDB = await dbContext.Habitacion.FirstOrDefaultAsync(h => h.Id == habitacion.Id);
                if (HabitacionDB != null)
                {
                    dbContext.Remove(HabitacionDB);
                    return await dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }

        public static async Task<HabitacionEN> GetHabitacionAsync(HabitacionEN habitacion)
        {
            using (var dbContext = new ContextDB())
            {
                return await dbContext.Habitacion.FirstOrDefaultAsync(h => h.Id == habitacion.Id);
            }
        }

        public static async Task<List<HabitacionEN>> GetHabitacionENAsync()
        {
            using (var dbContext = new ContextDB())
            {
                return await dbContext.Habitacion.ToListAsync();
            }
        }

        internal static IQueryable<HabitacionEN> QuerySelect(IQueryable<HabitacionEN> query, HabitacionEN habitacion)
        {
            if (habitacion.Id > 0)
            {
                query = query.Where(c => c.Id == habitacion.Id);
            }

            if (!string.IsNullOrEmpty(habitacion.NumeroDeHabitacion.ToString()))
            {
                query = query.OrderByDescending(c => c.Id);
            }

            return query;
        }
        public static async Task<List<HabitacionEN>> SearchAsync(HabitacionEN habitacion)
        {
            var HabitacionDB = new List<HabitacionEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Habitacion.AsQueryable();
                select = QuerySelect(select, habitacion);
                HabitacionDB = await select.ToListAsync();
            }
            return HabitacionDB;
        }
    }
}



