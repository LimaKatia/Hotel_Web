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
        private static async Task<bool> ExistsHabitacion(HabitacionEN habitacion, ContextDB dbContext)
        {
            bool result = false;
            var HabitacionExists = await dbContext.Habitacion.FirstOrDefaultAsync(h => h.NumeroDeHabitacion == habitacion.NumeroDeHabitacion && h.Id != habitacion.Id);
            if (HabitacionExists != null && HabitacionExists.Id > 0 && HabitacionExists.NumeroDeHabitacion == habitacion.NumeroDeHabitacion)
                result = true;
            return result;
        }
        public static async Task<int> CreateHabitacion(HabitacionEN habitacion)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                bool HabitacionExists = await ExistsHabitacion(habitacion, dbContext);
                if (HabitacionExists == false)
                {
                    // Agregar la nueva habitación
                    dbContext.Habitacion.Add(habitacion);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("La habitación ya existe");
                }
            }
            return result;
        }

        public static async Task<int> UpdateHabitacion(HabitacionEN habitacion)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var HabitacionDB = await dbContext.Habitacion.Include(h => h.state).Include(t => t.TipoDeHabitacion).FirstOrDefaultAsync(h => h.Id == habitacion.Id);
                if (HabitacionDB != null)
                {
                    HabitacionDB.NumeroDeHabitacion = habitacion.NumeroDeHabitacion;
                    HabitacionDB.Precio = habitacion.Precio;
                    HabitacionDB.Descripcion = habitacion.Descripcion;
                    HabitacionDB.IdEstado = habitacion.IdEstado;
                    HabitacionDB.IdTipoDeHabitacion = habitacion.IdTipoDeHabitacion;
                    dbContext.Update(HabitacionDB);
                    return await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<int> DeleteHabitacion(HabitacionEN habitacion)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var HabitacionDB = await dbContext.Habitacion.Include(h => h.state).Include(t => t.TipoDeHabitacion).FirstOrDefaultAsync(h => h.Id == habitacion.Id);
                if (HabitacionDB != null)
                {
                    dbContext.Remove(HabitacionDB);
                    return await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<HabitacionEN> GetHabitacionAsync(HabitacionEN habitacion)
        {
            var HabitacionDB = new HabitacionEN();
            using (var dbContext = new ContextDB())
            {
                HabitacionDB = await dbContext.Habitacion.Include(h => h.state).Include(t => t.TipoDeHabitacion).FirstOrDefaultAsync(h => h.Id == habitacion.Id);
            }
            return HabitacionDB;
        }

        public static async Task<List<HabitacionEN>> GetAllAsync()
        {
            var habitaciones = new List<HabitacionEN>();
            using (var dbContext = new ContextDB())
            {
                habitaciones = await dbContext.Habitacion.Include(h => h.state).Include(t => t.TipoDeHabitacion).ToListAsync();
            }
            return habitaciones;
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
            if (habitacion.Top_Aux > 0)
                query = query.Take(habitacion.Top_Aux).AsQueryable();
            return query;
        }
        public static async Task<List<HabitacionEN>> SearchAsync(HabitacionEN habitacion)
        {
            var HabitacionDB = new List<HabitacionEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Habitacion.Include(h => h.state).Include(t => t.TipoDeHabitacion).AsQueryable();
                select = QuerySelect(select, habitacion);
                HabitacionDB = await select.ToListAsync();
            }
            return HabitacionDB;
        }
    }

}



