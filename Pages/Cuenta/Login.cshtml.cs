using gamedestore.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace gamedestore.Pages.Cuenta
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;

        public LoginModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Correo { get; set; }
            public string Contrasena { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.Correo) || string.IsNullOrEmpty(Input.Contrasena))
            {
                ModelState.AddModelError(string.Empty, "Correo y contraseña son obligatorios!!");
                return Page();
            }

            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.correo == Input.Correo);

            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Credenciales invalidas.....");
                return Page();
            }

            if (usuario.contrasena == HashPassword(Input.Contrasena))
            {
                HttpContext.Session.SetString("nombreusuario", usuario.nombreusuario);

                
                HttpContext.Session.SetString("rolusuario", usuario.Rol);
                

                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Credenciales invalidas....");
                return Page();
            }
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
