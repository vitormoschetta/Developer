using Developer.Models;
using Microsoft.EntityFrameworkCore;

namespace Developer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }       

    }
}