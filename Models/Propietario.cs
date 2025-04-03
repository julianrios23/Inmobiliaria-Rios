using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Rios.Models
{
    [Table("propietarios")] // Mapea la tabla propietarios
    public class Propietario
    {
        [Column("idpropietario")] // Mapea la columna idpropietario
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Column("dni")]
        public int? DNI { get; set; } // Cambiado a int? porque puede ser NULL en la base de datos

        [Column("cuit")]
        public string CUIT { get; set; } = string.Empty;

        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Column("mail")]
        public string Mail { get; set; } = string.Empty;

        [Column("domicilio")]
        public string Domicilio { get; set; } = string.Empty;

        [Column("localidad")]
        public string Localidad { get; set; } = string.Empty;

        [Column("provincia")]
        public string Provincia { get; set; } = string.Empty;

        [Column("estado")]
        public bool Estado { get; set; } = true; // Mapeado como tinyint(1) en la base de datos
    }
}
