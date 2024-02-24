using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEN
{
    public class HabitacionEN
    {
        [Key]
        public byte Id { get; set; }

        [Required]
        public int NumeroDeHabitacion { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public byte IdEstado { get; set; }

        [Required]
        public byte IdTipoDeHabitacion { get; set; }

       

        [NotMapped]
        public int Top_Aux { get; set; }

    }
}
