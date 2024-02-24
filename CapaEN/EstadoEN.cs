using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEN
{
    public class EstadoEN
    {
        [Key]
        public byte Id { get; set; }

        [Display(Name = "Tipo de estado")]
        [Required(ErrorMessage ="El tipo de estado es requeido")]
        [StringLength(50,ErrorMessage ="Máximo 50 Carcteres")]
        public string Nombre { get; set; } = string.Empty;
    }
}
