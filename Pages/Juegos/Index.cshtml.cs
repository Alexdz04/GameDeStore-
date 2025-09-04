using gamedestore.Datos;
using gamedestore.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace gamedestore.Pages.Juegos
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;
        private readonly gamedestore.Modelos.Carrito _carrito;

        public IndexModel(ApplicationDbContext contexto, gamedestore.Modelos.Carrito carrito)
        {
            _contexto = contexto;
            _carrito = carrito;
        }

        public IList<Juego> ListaJuegos { get; set; }

        public async Task OnGetAsync()
        {
            ListaJuegos = await _contexto.Juegos.OrderBy(j => j.nombre).ToListAsync();
        }

        public async Task<IActionResult> OnPostAnadirAlCarritoAsync(int juegoId)
        {
           
            if (HttpContext.Session.GetString("nombreusuario") == null)
            {
                
                return RedirectToPage("/Cuenta/Login");
            }
            

            var juegoSeleccionado = await _contexto.Juegos.FindAsync(juegoId);
            if (juegoSeleccionado != null)
            {
                _carrito.AnadirItemAlCarrito(juegoSeleccionado, 1);
            }

            return RedirectToPage("/Carrito/Index");
        }
    }
}
