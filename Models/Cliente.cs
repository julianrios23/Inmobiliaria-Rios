using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Rios.Models
{
    [Table("clientes")] // Mapea la tabla clientes
    public class Cliente
    {
        [Key]
        [Column("idclientes")] // Mapea la columna idclientes
        public long Id { get; set; } // Cambiado de int a long

        [Column("nombre")] // Mapea la columna nombre
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")] // Mapea la columna apellido
        public string Apellido { get; set; } = string.Empty;

        [Column("dni")] // Mapea la columna dni
        public long DNI { get; set; } // Cambiado de int a long

        [Column("mail")] // Mapea la columna mail
        public string Email { get; set; } = string.Empty; // Asegurarse de que la propiedad esté definida correctamente

        [Column("telefono")] // Mapea la columna telefono
        public long Telefono { get; set; } // Cambiado de int a long

        [Column("domicilio")] // Mapea la columna domicilio
        public string Domicilio { get; set; } = string.Empty;

        [Column("localidad")] // Mapea la columna localidad
        public string Localidad { get; set; } = string.Empty;

        [Column("provincia")] // Mapea la columna provincia
        public string Provincia { get; set; } = string.Empty;

        [Column("estado")] // Mapea la columna estado
        public bool Estado { get; set; } // Cambiado de bool? a bool
    }
}
