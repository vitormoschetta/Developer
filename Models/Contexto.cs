using Microsoft.EntityFrameworkCore;

namespace Developer.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {  }
       
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        

        /*protected override void OnModelCreating(ModelBuilder modelo)
        {
            modelo.Entity<Usuario>().SeedData(
                new Usuario {Id=1, Nome="Admin", email="vitormoschetta@gmail.com", cpf="96332824204", 
                            Senha="Z6H8SPE1G4wcDtBjYFIlMpE3ZZmu6LeM3ep5wCcjgoQ=", 
                            Salt="9u8aFeTfqaMx03fBcVTrrQ==", Ativo='S', Perfil_Id=2});
            
            base.OnModelCreating(modelo); 
        }*/

    }
}