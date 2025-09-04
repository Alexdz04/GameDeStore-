using gamedestore.Datos;
using Microsoft.EntityFrameworkCore;

namespace gamedestore.Modelos
{
    public class Carrito
    {
        private readonly ApplicationDbContext _contexto;
        public string CarritoId { get; set; }
        public List<CarritoItem> CarritoItems { get; set; }

        public Carrito(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public static Carrito GetCarrito(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var contexto = services.GetService<ApplicationDbContext>();
            string carritoId = session.GetString("CarritoId") ?? Guid.NewGuid().ToString();
            session.SetString("CarritoId", carritoId);
            return new Carrito(contexto) { CarritoId = carritoId };
        }

        public void AnadirItemAlCarrito(Juego juego, int cantidad)
        {
            var carritoItem = _contexto.CarritoItems.SingleOrDefault(
                s => s.Juego.id == juego.id && s.CarritoId == CarritoId);

            if (carritoItem == null)
            {
                carritoItem = new CarritoItem
                {
                    CarritoId = CarritoId,
                    Juego = juego,
                    Cantidad = 1
                };
                _contexto.CarritoItems.Add(carritoItem);
            }
            else
            {
                carritoItem.Cantidad++;
            }
            _contexto.SaveChanges();
        }

        public List<CarritoItem> GetCarritoItems()
        {
            return CarritoItems ??= _contexto.CarritoItems.Where(c => c.CarritoId == CarritoId)
                .Include(s => s.Juego)
                .ToList();
        }

        public decimal GetCarritoTotal()
        {
            
            var total = _contexto.CarritoItems.Where(c => c.CarritoId == CarritoId)
                .Select(c => c.Juego.precio * c.Cantidad).ToList().Sum();
            return total;
            
        }
    }
}
