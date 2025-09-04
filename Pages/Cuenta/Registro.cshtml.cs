using gamedestore.Datos;
using gamedestore.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace gamedestore.Pages.Cuenta
{
    public class RegistroModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;

        public RegistroModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [BindProperty]
        public Usuario Usuario { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _contexto.Usuarios.AnyAsync(u => u.correo == Usuario.correo))
            {
                ModelState.AddModelError(string.Empty, "El correo electronico ya esta registrado!");
                return Page();
            }

            

            Usuario.contrasena = HashPassword(Usuario.contrasena);

            await _contexto.Usuarios.AddAsync(Usuario);
            await _contexto.SaveChangesAsync();

            return RedirectToPage("Login");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
