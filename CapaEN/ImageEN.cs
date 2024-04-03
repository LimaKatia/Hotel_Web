using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEN
{
    public class ImageEN
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "la imagen de Habitación es requerido")]
        [Display(Name = "Ruta")]
        public string UrlImage { get; set; } = string.Empty;

        [Required(ErrorMessage ="El número de habitación es requerida")]
        [ForeignKey("Habitacion")]
        [Display(Name ="Número de habitación")]
        public int IdHabitacion { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }
        public HabitacionEN Habitacion { get; set; } = new HabitacionEN();
    }
}
