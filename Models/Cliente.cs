using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Rios.Models
{
    [Table("clientes")] // Mapea la tabla clientes
    public class Cliente
    {
        [Key]
        [Column("idclientes")] // Mapea la columna idclientes
        public int Id { get; set; } // Clave primaria

        [Column("nombre")] // Mapea la columna nombre
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")] // Mapea la columna apellido
        public string Apellido { get; set; } = string.Empty;

        [Column("dni")] // Mapea la columna dni
        public int? DNI { get; set; } // Permitir valores NULL si la base de datos los contiene

        [Column("mail")] // Mapea la columna mail
        public string Email { get; set; } = string.Empty;

        [Column("telefono")] // Mapea la columna telefono
        public int? Telefono { get; set; } // Cambiado a int? para permitir valores NULL

        [Column("domicilio")] // Mapea la columna domicilio
        public string Domicilio { get; set; } = string.Empty;

        [Column("localidad")] // Mapea la columna localidad
        public string Localidad { get; set; } = string.Empty;

        [Column("provincia")] // Mapea la columna provincia
        public string Provincia { get; set; } = string.Empty;

        [Column("estado")] // Mapea la columna estado
        public bool? Estado { get; set; } // Cambiado a bool? para permitir valores NULL
    }
}
