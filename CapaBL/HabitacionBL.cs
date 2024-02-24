using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class HabitacionBL
    {
        public async Task<int> CreateHabitacion(HabitacionEN habitacionEN)
        {
            return await HabitacionDAL.CreateHabitacion(habitacionEN);
        }

        public async Task<int> UpdateHabitacion(HabitacionEN habitacionEN)
        {
            return await HabitacionDAL.UpdateHabitacion(habitacionEN);
        }

        public async Task<int> DeleteHabitacion(HabitacionEN habitacionEN)
        {
            return await HabitacionDAL.DeleteHabitacion(habitacionEN);
        }

        public async Task<HabitacionEN> GetHabitacionAsync(HabitacionEN habitacionEN)
        {
            return await HabitacionDAL.GetHabitacionAsync(habitacionEN);
        }

        public async Task<List<HabitacionEN>> GetHabitacionENAsync()
        {
            return await HabitacionDAL.GetHabitacionENAsync();
        }

        public async Task<List<HabitacionEN>> SearchAsync(HabitacionEN habitacionEN)
        {
            return await HabitacionDAL.SearchAsync(habitacionEN);
        }
    }
}
