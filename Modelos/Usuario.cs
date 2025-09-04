using System.ComponentModel.DataAnnotations;

namespace gamedestore.Modelos
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string nombreusuario { get; set; }

        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo electronico no valido!")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string contrasena { get; set; }

       
        public string? Rol { get; set; }
       
    }
}
