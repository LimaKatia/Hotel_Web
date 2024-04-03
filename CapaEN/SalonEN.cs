using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEN
{
    public class SalonEN
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del salon es requerido")]
        [Display(Name = "Nombre de Salon")]
        public string NombreSalon { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Precio es requerido")]
        [Column(TypeName ="decimaml(16,2)")]
        [DisplayFormat(DataFormatString ="{0:c2}")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage ="Por favor, ingrese un número válido para el precio")]
        public decimal Precio { get; set; }

        [MaxLength(100, ErrorMessage ="Máximo de 100 caracteres")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="La descripción es requerida")]
        [Display(Name ="Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Tipo de estado es Requerido")]
        [ForeignKey("state")]
        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El Tipo de salon es requerido")]
        [ForeignKey("TipoDeSalon")]
        [Display(Name = "Tipo de salon")]
        public int IdTipoDeSalon { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }
        public EstadoEN? state { get; set; }
        public TipoDeSalonEN? TipoDeSalon { get; set; }
        public List<ImagenSalonEN> image { get; set; } = new List<ImagenSalonEN>();
    }
}
