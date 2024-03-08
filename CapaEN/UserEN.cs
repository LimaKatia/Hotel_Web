using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CapaEN
{
    public class UserEN
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Role")]
        [Required(ErrorMessage = "El rol es requerido")]
        [Display(Name = "Rol")]
        public int IdRole { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        [MaxLength(30, ErrorMessage = "Másximo 30 cáracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(30, ErrorMessage = "Másximo 30 cáracteres")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del usuario es requerido")]
        [MaxLength(25, ErrorMessage = "Másximo 25 cáracteres")]
        [Display(Name = "Nombre de usuario")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "La contraseña debe estar entre 6 y 32 caracteres", MinimumLength = 6)]//maxiomo y minimo de carácteres
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es requerido")]
        [Display(Name = "Estado")]
        public byte Status { get; set; }

        [Display(Name = "Fecha de registro")]
        public DateTime RegistrationDate { get; set; }

        ///--------------------------------------------------///
        [NotMapped]
        public int Top_Aux { get; set; }//propiedad auxiliar

        [NotMapped]
        [Required(ErrorMessage = "la confirmación de la contraseña es requerida")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña no coinciden")]
        [StringLength(32, ErrorMessage = "La contraseña debe estar entre 6 y 32 caracteres", MinimumLength = 6)]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword_Aux { get; } = string.Empty; //propiedad auxiliar
        public RoleEN? Role { get; set; }//propiedad de navegación
    }

    public enum User_Status
    {
        ACTIVO = 1, INACTIVO = 2
    }
}
