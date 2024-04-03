using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEN
{
    public class ImagenSalonEN
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "la imagen del Salón es requerido")]
        [Display(Name = "Ruta")]
        public string UrlImage { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de salón es requerida")]
        [ForeignKey("Salon")]
        [Display(Name = "Número de salón")]
        public int IdSalon { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }
        public SalonEN Salon { get; set; } = new SalonEN();
    }
}
