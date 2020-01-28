using Microsoft.EntityFrameworkCore;

namespace Developer.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<OriginTable> OriginTable { get; set; }
        public DbSet<DestinationTable> DestinationTable { get; set; }
        
    }
}