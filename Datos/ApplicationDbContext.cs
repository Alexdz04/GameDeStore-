using gamedestore.Modelos;
using Microsoft.EntityFrameworkCore;

namespace gamedestore.Datos
{

    public class ApplicationDbContext : DbContext
    {
     
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Juego> Juegos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<CarritoItem> CarritoItems { get; set; }
    }
}
