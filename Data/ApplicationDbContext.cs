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
        public DbSet<Propietario> Propietarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; } // Agregado para la tabla clientes
        public DbSet<ImagenInmueble> ImagenesInmuebles { get; set; }
        public DbSet<Contrato> Contratos { get; set; } // Agrega esta línea

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la tabla inmuebles
            modelBuilder.Entity<Propiedad>().ToTable("inmuebles");

            modelBuilder.Entity<Propiedad>()
                .Property(e => e.Tipo)
                .HasConversion<string>();

            modelBuilder.Entity<Propiedad>()
                .Property(e => e.Garage)
                .HasConversion<string>();

            // Configuración para la tabla propietarios
            modelBuilder.Entity<Propietario>().ToTable("propietarios");

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
