using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Inmobiliaria_Rios.Models
{
    [Table("inmuebles")]
    public class Propiedad
    {
        [Key]
        [Column("idinmuebles")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Propietario es obligatorio.")]
        [Column("idpropietario")]
        public int IdPropietario { get; set; }

        [ForeignKey("IdPropietario")]
        public Propietario? Propietario { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; } = string.Empty;

        [Column("localidad")]
        public string Localidad { get; set; } = string.Empty;

        [Column("provincia")]
        public string Provincia { get; set; } = string.Empty;

        [Column("tipo")]
        public string Tipo { get; set; } = string.Empty;

        [Column("ambientes")]
        public int Ambientes { get; set; }

        [Column("banos")]
        public int Banos { get; set; }

        [Column("garage")]
        public string Garage { get; set; } = "no";

        [Column("capGarage")]
        public int? CapGarage { get; set; }

        [Column("patio")]
        public string Patio { get; set; } = "no";

        [Column("piscina")]
        public string Piscina { get; set; } = "no";

        [Column("terraza")]
        public string Terraza { get; set; } = "no";

        [Column("plantas")]
        public int Plantas { get; set; }

        [Column("cocina")]
        public string Cocina { get; set; } = "no";

        [Column("parrilla")]
        public string Parrilla { get; set; } = "no";

        [Column("opcion")]
        public string Opcion { get; set; } = string.Empty;

        [Column("precio")]
        public int Precio { get; set; }

        [Column("observaciones")]
        public string? Observaciones { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;

        public ICollection<ImagenInmueble> Imagenes { get; set; } = new List<ImagenInmueble>(); // Relación con las imágenes
    }
}
