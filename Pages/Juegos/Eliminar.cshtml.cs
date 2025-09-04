using gamedestore.Datos;
using gamedestore.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gamedestore.Pages.Juegos
{
    public class EliminarModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;

        public EliminarModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [BindProperty]
        public Juego Juego { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            
            if (HttpContext.Session.GetString("rolusuario") != "Administrador")
            {
                return RedirectToPage("/Index");
            }
            

            Juego = await _contexto.Juegos.FindAsync(id);

            if (Juego == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var juegoaeliminar = await _contexto.Juegos.FindAsync(Juego.id);

            if (juegoaeliminar == null)
            {
                return NotFound();
            }

            _contexto.Juegos.Remove(juegoaeliminar);
            await _contexto.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
