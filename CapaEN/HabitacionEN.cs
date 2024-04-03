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
        public int Id { get; set; }

        [Required(ErrorMessage = "El Numero de Habitación es requerido")]
        [Display(Name = "Numero de Habitación")]
        public int NumeroDeHabitacion { get; set; }

        [Required(ErrorMessage = "El Precio es requerido")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Por favor, ingrese un número válido para el precio.")]
        public decimal Precio { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "La descripción es requerida")]
        [Display(Name ="Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Tipo de estado es Requerido")]
        [ForeignKey("state")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El Tipo de Habitación es requerido")]
        [ForeignKey("TipoDeHabitacion")]
        [Display(Name = "Tipo de Habitación")]
        public int IdTipoDeHabitacion { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public EstadoEN? state { get; set; } 
        public TipoHabitacionEN? TipoDeHabitacion { get; set; }
        public List<ImageEN> image { get; set; } = new List<ImageEN>(); //Propiedad de navegacion
    }
}
