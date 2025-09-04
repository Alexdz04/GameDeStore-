using gamedestore.Datos;
using gamedestore.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gamedestore.Pages.Carrito
{
    public class IndexModel : PageModel
    {
        private readonly gamedestore.Modelos.Carrito _carrito;
        private readonly ApplicationDbContext _contexto;

        public IndexModel(gamedestore.Modelos.Carrito carrito, ApplicationDbContext contexto)
        {
            _carrito = carrito;
            _contexto = contexto;
        }

        public gamedestore.Modelos.Carrito Carrito { get; set; }
        public decimal CarritoTotal { get; set; }

        public void OnGet()
        {
            Carrito = _carrito;
            Carrito.CarritoItems = _carrito.GetCarritoItems();
            CarritoTotal = _carrito.GetCarritoTotal();
        }

        public async Task<IActionResult> OnPostEliminarDelCarritoAsync(int itemId)
        {
            var itemAEliminar = await _contexto.CarritoItems.FindAsync(itemId);
            if (itemAEliminar != null)
            {
                _contexto.CarritoItems.Remove(itemAEliminar);
                await _contexto.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostComprar()
        {
            var itemsDelCarrito = _contexto.CarritoItems.Where(c => c.CarritoId == _carrito.CarritoId);
            _contexto.CarritoItems.RemoveRange(itemsDelCarrito);
            _contexto.SaveChanges();

            TempData["MensajeCompra"] = "Gracias por tu compra!!";

            
            return RedirectToPage();
            
        }
    }
}
