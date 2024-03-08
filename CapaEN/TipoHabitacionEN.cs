using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEN
{
    public class TipoHabitacionEN
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tipo de habitación")]
        [Required(ErrorMessage = "El tipo de habitación es requeido")]
        [StringLength(50, ErrorMessage = "Máximo 50 Carcteres")]
        public string Nombre { get; set; } = string.Empty;

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
