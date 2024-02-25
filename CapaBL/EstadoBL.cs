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
        public static async Task<int> CreateState (EstadoEN estadoEN)
        {
            return await EstadoDAL.CreateState (estadoEN);
        }
    }
}
