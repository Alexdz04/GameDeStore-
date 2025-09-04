
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gamedestore.Modelos
{
    public class Juego
    {

        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre del juego es obligatorio!")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener mas de 100 caracteres....")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria!!")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio...")]
     
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal precio { get; set; }

        [Required(ErrorMessage = "La fecha de lanzamiento es obligatoria!!")]
        [DataType(DataType.Date)]
        public DateTime fechalanzamiento { get; set; }

        [Required(ErrorMessage = "El genero es obligatorio....")]
        public string genero { get; set; }

        public string imagenurl { get; set; }
    }
}
