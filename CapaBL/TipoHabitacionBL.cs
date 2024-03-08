using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class TipoHabitacionBL
    {
        public async Task<int> CreateTipo(TipoHabitacionEN habitacionEN)
        {
            return await TipoHabitacionDAL.CreateTipo(habitacionEN);
        }
        public async Task<int> UpdateAsync(TipoHabitacionEN role)
        {
            return await TipoHabitacionDAL.UpdateTipo(role);
        }

        public async Task<int> DeleteAsync(TipoHabitacionEN role)
        {
            return await TipoHabitacionDAL.DeleteTipo(role);
        }

        public async Task<TipoHabitacionEN> GetByIdAsync(TipoHabitacionEN role)
        {
            return await TipoHabitacionDAL.GetByIdAsync(role);
        }

        public async Task<List<TipoHabitacionEN>> GetAllAsync()
        {
            return await TipoHabitacionDAL.GetAllAsync();
        }

        public async Task<List<TipoHabitacionEN>> SearchAsync(TipoHabitacionEN role)
        {
            return await TipoHabitacionDAL.SearchAsync(role);
        }

    }
}
