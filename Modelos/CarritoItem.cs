using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gamedestore.Modelos
{
    public class CarritoItem
    {
        [Key]
        public int Id { get; set; }

        public int JuegoId { get; set; }
        [ForeignKey("JuegoId")]
        public Juego Juego { get; set; }

        public int Cantidad { get; set; }


        public string CarritoId { get; set; }
    }
}
