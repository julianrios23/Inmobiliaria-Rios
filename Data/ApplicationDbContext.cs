using Microsoft.EntityFrameworkCore;
using Inmobiliaria_Rios.Models;

namespace Inmobiliaria_Rios.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Propiedad> Propiedades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Propiedad>()
                .Property(e => e.Tipo)
                .HasConversion<string>();

            modelBuilder.Entity<Propiedad>()
                .Property(e => e.Garage)
                .HasConversion<string>();

            modelBuilder.Entity<Propiedad>()
                .Property(e => e.Patio)
                .HasConversion<string>();

            modelBuilder.Entity<Propiedad>()
                .Property(e => e.Piscina)
                .HasConversion<string>();

            modelBuilder.Entity<Propiedad>()
                .Property(e => e.Terraza)
                .HasConversion<string>();

            modelBuilder.Entity<Propiedad>()
                .Property(e => e.Cocina)
                .HasConversion<string>();

            modelBuilder.Entity<Propiedad>()
                .Property(e => e.Parrilla)
                .HasConversion<string>();
        }
    }
}
