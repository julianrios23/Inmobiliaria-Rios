using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Rios.Models
{
    public enum TipoPropiedad : byte
    {
        casa = 0,
        departamento = 1,
        local = 2,
        lote = 3,
        cabana = 4
    }

    public enum EstadoSiNo : byte
    {
        no = 0,
        si = 1
    }

    [Table("inmuebles")] // Mapea la tabla inmuebles
    public class Propiedad
    {
        [Column("idinmuebles")] // Mapea la columna idinmuebles
        public int Id { get; set; }

        [Column("idpropietario")] // Mapea la columna idpropietario
        public int IdPropietario { get; set; }

        public required string Direccion { get; set; }
        public required string Localidad { get; set; }
        public required string Provincia { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Tipo { get; set; } = string.Empty; // Cambiado a string si en la base de datos es varchar

        public required int Ambientes { get; set; }
        public required int Banos { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string Garage { get; set; } = "no"; // Cambiado a string si en la base de datos es varchar

        [Column("capGarage")]
        public int? CapGarage { get; set; } = null;

        [Column(TypeName = "varchar(2)")]
        public string Patio { get; set; } = "no"; // Cambiado a string si en la base de datos es varchar

        [Column(TypeName = "varchar(2)")]
        public string Piscina { get; set; } = "no"; // Cambiado a string si en la base de datos es varchar

        [Column(TypeName = "varchar(2)")]
        public string Terraza { get; set; } = "no"; // Cambiado a string si en la base de datos es varchar

        public required int Plantas { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string Cocina { get; set; } = "no"; // Cambiado a string si en la base de datos es varchar

        [Column(TypeName = "varchar(2)")]
        public string Parrilla { get; set; } = "no"; // Cambiado a string si en la base de datos es varchar

        public required string Opcion { get; set; }
        public required int Precio { get; set; }
        public string? Observaciones { get; set; }

        public bool Estado { get; set; } = true;

        // Constructor sin parámetros para Entity Framework
        public Propiedad() { }
    }
}
