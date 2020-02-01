using Microsoft.EntityFrameworkCore;

namespace Developer.Models
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {        }

        public DbSet<Menu> Menu { get; set; }
        public DbSet<Table> Table { get; set; }

        
/*
        protected void ConfiguraMenu(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.Entity<Menu>( m =>
            {
                m.ToTable("Cliente");
                m.HasKey(c => c.Id).HasName("id");
                m.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
                m.Property(c => c.Nome).HasColumnName("nome").HasMaxLength(100);
                m.Property(c => c.Formulario).HasColumnName("formulario").HasMaxLength(100);
                m.Property(c => c.Funcao).HasColumnName("funcao").HasMaxLength(500);
                m.Property(c => c.Link).HasColumnName("link").HasMaxLength(200);
                m.Property(c => c.Back).HasColumnName("back").HasMaxLength(30);
                m.Property(c => c.Front).HasColumnName("front").HasMaxLength(30);
                m.Property(c => c.Layout).HasColumnName("layout").HasMaxLength(30);
                m.Property(c => c.Obs).HasColumnName("obs").HasMaxLength(500);
                m.Property(c => c.OriginTable).HasColumnName("origin_table").HasMaxLength(50);
                m.Property(c => c.DestinTable).HasColumnName("destin_table").HasMaxLength(50);
                m.Property(c => c.ObsTable).HasColumnName("obs_table").HasMaxLength(50);
                m.Property(c => c.MenuPai).HasColumnName("menu_pai").HasMaxLength(50);
            });
        }


        protected void ConfiguraTable(ModelBuilder construtorDeModelos)
        {
            construtorDeModelos.Entity<Table>( m =>
            {
                m.ToTable("Table");
                m.HasKey(c => c.Id).HasName("id");
                m.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
                m.Property(c => c.Nome).HasColumnName("nome").HasMaxLength(100);
                m.Property(c => c.Menu).HasColumnName("menu_Id").HasMaxLength(100);
                m.Property(c => c.Menu).IsRequired();                
            });
        }

        protected override void OnModelCreating(ModelBuilder construtorDeModelos)
        {
            ConfiguraMenu(construtorDeModelos);
            ConfiguraTable(construtorDeModelos);
        }*/
    }
}