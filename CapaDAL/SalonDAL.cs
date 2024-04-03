using CapaEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class SalonDAL
    {
        private static async Task<bool> ExistsSalon(SalonEN salon, ContextDB dbContext)
        {
            bool result = false;
            var salonExists = await dbContext.Salon.FirstOrDefaultAsync(h => h.NombreSalon == salon.NombreSalon && h.Id != salon.Id);
            if (salonExists != null && salonExists.Id > 0 && salonExists.NombreSalon == salon.NombreSalon)
                result = true;
            return result;
        }

        public static async Task<int> CreateSalon(SalonEN salon)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                bool HabitacionExists = await ExistsSalon(salon, dbContext);
                if (HabitacionExists == false)
                {
                    // Agregar la nueva habitación
                    dbContext.Salon.Add(salon);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Salon ya existente");
                }
            }
            return result;
        }

        public static async Task<int> UpdateSalon(SalonEN salon)
        {
            using (var dbContext = new ContextDB())
            {
                var salonDB = await dbContext.Salon.FirstOrDefaultAsync(s => s.Id == salon.Id);
                if (salonDB != null)
                {
                    salonDB.NombreSalon = salon.NombreSalon;
                    salonDB.Precio = salon.Precio;
                    salonDB.Descripcion = salon.Descripcion;
                    salonDB.IdEstado = salon.IdEstado;
                    salonDB.IdTipoDeSalon = salon.IdTipoDeSalon;
                    return await dbContext.SaveChangesAsync();
                }
                return 0; // Si no se encuentra el salón, retornar 0
            }
        }

        public static async Task<int> DeleteSalon(SalonEN salon)
        {
            using (var dbContext = new ContextDB())
            {
                var salonDB = await dbContext.Salon.FirstOrDefaultAsync(h => h.Id == salon.Id);
                if (salonDB != null)
                {
                    dbContext.Remove(salonDB);
                    return await dbContext.SaveChangesAsync();
                }
                return 0; // Si no se encuentra el salón, retornar 0
            }
        }

        public static async Task<SalonEN> GetSalonAsync(SalonEN salon)
        {
            var salonDB = new SalonEN();
            using (var dbContext = new ContextDB())
            {
                salonDB = await dbContext.Salon.Include(h => h.state).Include(t => t.TipoDeSalon).FirstOrDefaultAsync(h => h.Id == salon.Id);
            }
            return salonDB;
        }

        public static async Task<List<SalonEN>> GetAllAsync()
        {
            var salones = new List<SalonEN>();
            using (var dbContext = new ContextDB())
            {
                salones = await dbContext.Salon.Include(h => h.state).Include(t => t.TipoDeSalon).ToListAsync();
            }
            return salones;
        }

        internal static IQueryable<SalonEN> QuerySelect(IQueryable<SalonEN> query, SalonEN salon)
        {
            if (salon.Id > 0)
            {
                query = query.Where(c => c.Id == salon.Id);
            }

            if (!string.IsNullOrEmpty(salon.NombreSalon.ToString()))
            {
                query = query.OrderByDescending(c => c.Id);
            }
            if (salon.Top_Aux > 0)
                query = query.Take(salon.Top_Aux).AsQueryable();
            return query;
        }

        public static async Task<List<SalonEN>> SearchAsync(SalonEN salon)
        {
            var tipos = new List<SalonEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Salon.Include(h => h.state).Include(t => t.TipoDeSalon).AsQueryable();
                select = QuerySelect(select, salon);
                tipos = await select.ToListAsync();
            }
            return tipos;
        }
    }
}
