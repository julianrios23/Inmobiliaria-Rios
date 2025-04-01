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

    public class Propiedad
    {
        public int Id { get; set; }
        public required string Direccion { get; set; }
        public required string Localidad { get; set; }
        public required string Provincia { get; set; }

        [Column(TypeName = "varchar(20)")]
        public TipoPropiedad Tipo { get; set; }

        public required int Ambientes { get; set; }
        public required int Banos { get; set; }

        [Column(TypeName = "varchar(2)")]
        public EstadoSiNo Garage { get; set; } = EstadoSiNo.no;
        public int? CapGarage { get; set; } = null;

        [Column(TypeName = "varchar(2)")]
        public EstadoSiNo Patio { get; set; }
        [Column(TypeName = "varchar(2)")]
        public EstadoSiNo Piscina { get; set; }
        [Column(TypeName = "varchar(2)")]
        public EstadoSiNo Terraza { get; set; }
        public required int Plantas { get; set; }

        [Column(TypeName = "varchar(2)")]
        public EstadoSiNo Cocina { get; set; }
        [Column(TypeName = "varchar(2)")]
        public EstadoSiNo Parrilla { get; set; }
        public required string Opcion { get; set; }
        public required int Precio { get; set; }
        public string? Observaciones { get; set; }
        public bool Estado { get; set; } = true;

        // Constructor sin parámetros para Entity Framework
        public Propiedad() { }
    }
}
