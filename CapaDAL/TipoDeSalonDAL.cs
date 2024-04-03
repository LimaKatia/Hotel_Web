using CapaEN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class TipoDeSalonDAL
    {
        public static async Task<int> CreateTipoDeSalon(TipoDeSalonEN tipoEN)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(tipoEN);
                return await dbContext.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateTipoDeSalon(TipoDeSalonEN tipodesalonEN)
        {
            using (var dbContext = new ContextDB())
            {
                var TipoDB = await dbContext.TipoDeSalon.FirstOrDefaultAsync(e => e.Id == tipodesalonEN.Id);
                if (TipoDB != null)
                {
                    TipoDB.Nombre = TipoDB.Nombre;
                    return await dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }
        public static async Task<int> DeleteTipoDeSalon(TipoDeSalonEN tipodesalonEN)
        {
            using (var dbContext = new ContextDB())
            {
                var TipoDB = await dbContext.TipoDeSalon.FirstOrDefaultAsync(e => e.Id == tipodesalonEN.Id);
                if (TipoDB != null)
                {
                    dbContext.Remove(TipoDB);
                    return await dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }
        public static async Task<TipoDeSalonEN> GetByIdAsync(TipoDeSalonEN tipodesalonEN)
        {
            var tipoDB = new TipoDeSalonEN();
            using (var dbContext = new ContextDB())
            {
                tipoDB = await dbContext.TipoDeSalon.FirstOrDefaultAsync(t => t.Id == tipodesalonEN.Id);

            }
            return tipoDB;
        }

        public static async Task<List<TipoDeSalonEN>> GetAllAsync()
        {
            var salones = new List<TipoDeSalonEN>();
            using (var dbContext = new ContextDB())
            {
                salones = await dbContext.TipoDeSalon.ToListAsync();
            }
            return salones;
        }
        internal static IQueryable<TipoDeSalonEN> QuerySelect(IQueryable<TipoDeSalonEN> query, TipoDeSalonEN TipoDeSalon)
        {
            if (TipoDeSalon.Id > 0)
                query = query.Where(r => r.Id == TipoDeSalon.Id);

            if (!string.IsNullOrWhiteSpace(TipoDeSalon.Nombre))
                query = query.Where(r => r.Nombre.Contains(TipoDeSalon.Nombre));

            query = query.OrderByDescending(r => r.Id).AsQueryable();

            if (TipoDeSalon.Top_Aux > 0)
                query = query.Take(TipoDeSalon.Top_Aux).AsQueryable();

            return query;
        }
        public static async Task<List<TipoDeSalonEN>> SearchAsync(TipoDeSalonEN TipoDeSalon)
        {
            var tipos = new List<TipoDeSalonEN>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.TipoDeSalon.AsQueryable();
                select = QuerySelect(select, TipoDeSalon);
                tipos = await select.ToListAsync();
            }
            return tipos;
        }
    }
}
