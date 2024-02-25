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

        [Required(ErrorMessage = "El Numero de Habitación es requerido")]
        [Display(Name = "Numero de Habitación")]
        public int NumeroDeHabitacion { get; set; }

        [Required(ErrorMessage = "El Precio es requerido")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El Tipo de estado es Requerido")]
        [ForeignKey("state")]
        [Display(Name = "Estado")]
        public byte IdEstado { get; set; }

        [Required(ErrorMessage = "El Tipo de Habitación es requerido")]
        [ForeignKey("TipoDeHabitacion")]
        [Display(Name = "Tipo de Habitación")]
        public byte IdTipoDeHabitacion { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
        public EstadoEN state { get; set; }
        public TipoHabitacionEN TipoDeHabitacion { get;set;}
    }
}
