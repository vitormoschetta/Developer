using Microsoft.EntityFrameworkCore;

namespace Developer.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {        }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }
}