using Microsoft.EntityFrameworkCore;

namespace Inmobiliaria_Rios.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Propiedades = Set<Propiedad>(); // Inicializar la propiedad
        }

        public DbSet<Propiedad> Propiedades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Propiedad>(entity =>
            {
                entity.ToTable("inmuebles"); // Configurar el nombre de la tabla
                entity.Property(e => e.Id).HasColumnName("idinmuebles");
                entity.Property(e => e.Direccion).HasColumnName("direccion");
                entity.Property(e => e.Localidad).HasColumnName("localidad");
                entity.Property(e => e.Provincia).HasColumnName("provincia");
                entity.Property(e => e.Tipo).HasColumnName("tipo");
                entity.Property(e => e.Ambientes).HasColumnName("ambientes");
                entity.Property(e => e.Banos).HasColumnName("banos");
                entity.Property(e => e.Garage).HasColumnName("garage").HasConversion<string>(); // Mapeo de Enum a String
                entity.Property(e => e.CapGarage).HasColumnName("capGarage");
                entity.Property(e => e.Patio).HasColumnName("patio").HasConversion<string>(); // Mapeo de Enum a String
                entity.Property(e => e.Piscina).HasColumnName("piscina").HasConversion<string>(); // Mapeo de Enum a String
                entity.Property(e => e.Terraza).HasColumnName("terraza").HasConversion<string>(); // Mapeo de Enum a String
                entity.Property(e => e.Plantas).HasColumnName("plantas");
                entity.Property(e => e.Cocina).HasColumnName("cocina").HasConversion<string>(); // Mapeo de Enum a String
                entity.Property(e => e.Parrilla).HasColumnName("parrilla").HasConversion<string>(); // Mapeo de Enum a String
                entity.Property(e => e.Observaciones).HasColumnName("observaciones");
                entity.Property(e => e.Opcion).HasColumnName("opcion").HasConversion<string>(); // Mapeo de Enum a String
                entity.Property(e => e.Precio).HasColumnName("precio"); // Mapeo de la columna precio
            });
        }
    }
}
