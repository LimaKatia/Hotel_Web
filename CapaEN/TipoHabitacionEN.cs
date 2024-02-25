using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEN
{
    public class TipoHabitacionEN
    {
        [Key]
        public byte Id { get; set; }

        [Display(Name = "Tipo de habitación")]
        [Required(ErrorMessage = "El tipo de habitación es requeido")]
        [StringLength(50, ErrorMessage = "Máximo 50 Carcteres")]
        public string Nombre { get; set; } = string.Empty;
    }
}
