using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Rios.Models
{
    [Table("propietarios")]
    public class Propietario
    {
        [Key]
        [Column("idpropietario")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Column("dni")]
        public int DNI { get; set; } // Cambiado de Dni a DNI

        [Column("cuit")]
        public string? Cuit { get; set; }

        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("mail")]
        public string? Mail { get; set; }

        [Column("domicilio")]
        public string? Domicilio { get; set; }

        [Column("localidad")]
        public string? Localidad { get; set; }

        [Column("provincia")]
        public string? Provincia { get; set; }

        [Column("estado")]
        public bool Estado { get; set; } = true;
    }
}
