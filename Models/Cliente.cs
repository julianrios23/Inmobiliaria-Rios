using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Rios.Models
{
    [Table("clientes")] // Mapea la tabla clientes
    public class Cliente
    {
        [Column("idclientes")] // Mapea la columna idclientes
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Column("dni")]
        public int? DNI { get; set; } // Cambiado a int? porque puede ser NULL en la base de datos

        [Column("cuit")]
        public string CUIT { get; set; } = string.Empty;

        [Column("mail")]
        public string Email { get; set; } = string.Empty; // Cambiado de "Mail" a "Email" para coincidir con la vista

        [Column("telefono")]
        public int? Telefono { get; set; } // Cambiado a int? porque puede ser NULL en la base de datos

        [Column("domicilio")]
        public string Domicilio { get; set; } = string.Empty;

        [Column("localidad")]
        public string Localidad { get; set; } = string.Empty;

        [Column("provincia")]
        public string Provincia { get; set; } = string.Empty;

        [Column("estado")]
        public bool? Estado { get; set; } // Cambiado a bool? porque puede ser NULL en la base de datos
    }
}
