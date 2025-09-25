using Microsoft.EntityFrameworkCore;
using MovieStore.Data.Entities;

namespace MovieStore.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Nome).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.Property(u => u.Login).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Senha).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
                entity.Property(c => c.CPF).IsRequired().HasMaxLength(14);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Endereco).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Telefone).HasMaxLength(15);
            });

            modelBuilder.Entity<Filme>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Titulo).IsRequired().HasMaxLength(150);
                entity.Property(f => f.Descricao).HasMaxLength(500);
                entity.Property(f => f.Genero).IsRequired().HasMaxLength(50);
                entity.Property(f => f.Diretor).IsRequired().HasMaxLength(100);
                entity.Property(f => f.ValorLocacao).HasPrecision(10, 2);
            });

            modelBuilder.Entity<Locacao>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Valor).HasPrecision(10, 2);
                entity.Property(l => l.ValorMulta).HasPrecision(10, 2);
                entity.Property(l => l.Observacoes).HasMaxLength(500);

                entity.HasOne(l => l.Cliente)
                      .WithMany(c => c.Locacoes)
                      .HasForeignKey(l => l.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Filme)
                      .WithMany(f => f.Locacoes)
                      .HasForeignKey(l => l.FilmeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
