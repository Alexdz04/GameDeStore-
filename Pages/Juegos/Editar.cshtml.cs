using gamedestore.Datos;
using gamedestore.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace gamedestore.Pages.Juegos
{
    public class EditarModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;

        public EditarModel(ApplicationDbContext contexto)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _contexto.Attach(Juego).State = EntityState.Modified;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _contexto.Juegos.AnyAsync(e => e.id == Juego.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }
    }
}
