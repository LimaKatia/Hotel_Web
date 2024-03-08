using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class EstadoBL
    {

        public static async Task<int> CreateState(EstadoEN estadoEN)
        {
            return await EstadoDAL.CreateState(estadoEN);
        }

        public async Task<int> UpdateEstado(EstadoEN estadoEN)
        {
            return await EstadoDAL.UpdateEstado (estadoEN);
        }

        public async Task<int> DeleteEstado(EstadoEN estadoEN)
        {
            return await EstadoDAL.DeleteEstado(estadoEN);
        }
        public async Task<EstadoEN> GetEstadoAsync(EstadoEN estadoEN)
        {
            return await EstadoDAL.GetEstadoAsync(estadoEN);
        }

        public async Task<List<EstadoEN>> GetAllAsync()
        {
            return await EstadoDAL.GetAllAsync();
        }
        public async Task<List<EstadoEN>> SearchAsync(EstadoEN estadoEN)
        {
            return await EstadoDAL.SearchAsync(estadoEN);
        }
    }
}
