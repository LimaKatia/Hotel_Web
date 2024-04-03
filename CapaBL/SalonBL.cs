using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class SalonBL
    {
        public async Task<int> CreateSalon(SalonEN salonEN)
        {
            return await SalonDAL.CreateSalon(salonEN);
        }

        public async Task<int> UpdateHabitacion(SalonEN salonEN)
        {
            return await SalonDAL.UpdateSalon(salonEN);
        }

        public async Task<int> DeleteSalon(SalonEN salonEN)
        {
            return await SalonDAL.DeleteSalon(salonEN);
        }

        public async Task<SalonEN> GetSalonAsync(SalonEN salonEN)
        {
            return await SalonDAL.GetSalonAsync(salonEN);
        }

        public async Task<List<SalonEN>> GetAllAsync()
        {
            return await SalonDAL.GetAllAsync();
        }

        public async Task<List<SalonEN>> SearchAsync(SalonEN salon)
        {
            return await SalonDAL.SearchAsync(salon);
        }
    }
}
