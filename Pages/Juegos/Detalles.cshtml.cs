using gamedestore.Datos;
using gamedestore.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gamedestore.Pages.Juegos
{
    public class DetallesModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;

        public DetallesModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public Juego Juego { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Juego = await _contexto.Juegos.FindAsync(id);

            if (Juego == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
