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

       
    }
}
