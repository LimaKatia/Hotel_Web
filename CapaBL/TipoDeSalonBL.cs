using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDAL;
using CapaEN;

namespace CapaBL
{
    public class TipoDeSalonBL
    {
        public async Task<int> CreateTipoDeSalon(TipoDeSalonEN tipoDeSalonEN)
        {
            return await TipoDeSalonDAL.CreateTipoDeSalon(tipoDeSalonEN);
        }
        public async Task<int> UpdateAsync(TipoDeSalonEN tipoDeSalonEN)
        {
            return await TipoDeSalonDAL.UpdateTipoDeSalon(tipoDeSalonEN);
        }

        public async Task<int> DeleteAsync(TipoDeSalonEN tipoDeSalonEN)
        {
            return await TipoDeSalonDAL.DeleteTipoDeSalon(tipoDeSalonEN);
        }

        public async Task<TipoDeSalonEN> GetByIdAsync(TipoDeSalonEN tipoDeSalonEN)
        {
            return await TipoDeSalonDAL.GetByIdAsync(tipoDeSalonEN);
        }

        public async Task<List<TipoDeSalonEN>> GetAllAsync()
        {
            return await TipoDeSalonDAL.GetAllAsync();
        }

        public async Task<List<TipoDeSalonEN>> SearchAsync(TipoDeSalonEN tipoDeSalonEN)
        {
            return await TipoDeSalonDAL.SearchAsync(tipoDeSalonEN);
        }
    }
}
