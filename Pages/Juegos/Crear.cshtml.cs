using gamedestore.Datos;
using gamedestore.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gamedestore.Pages.Juegos
{
    public class CrearModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;

        public CrearModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [BindProperty]
        public Juego Juego { get; set; }

        public IActionResult OnGet()
        {
            
            if (HttpContext.Session.GetString("rolusuario") != "Administrador")
            {
                
                return RedirectToPage("/Index");
            }
            

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _contexto.Juegos.AddAsync(Juego);
            await _contexto.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
