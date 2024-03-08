using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CapaEN
{
    public class RoleEN
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(30, ErrorMessage = "Máximo 30 crácteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [NotMapped]
        public int Top_Aux { get; set; }//propiedad auxiliar
        public List<UserEN>? Users { get; set; }//propiedad de navegación
    }
}
