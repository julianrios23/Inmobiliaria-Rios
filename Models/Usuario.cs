using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Rios.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("idusuarios")]
        public int Idusuarios { get; set; }

        [Column("nombre")]
        [StringLength(45)]
        public string? Nombre { get; set; }

        [Column("apellido")]
        [StringLength(45)]
        public string? Apellido { get; set; }

        [Column("telefono")]
        public int? Telefono { get; set; }

        [Column("mail")]
        [StringLength(45)]
        public string? Mail { get; set; }

        [Column("rol")]
        [StringLength(10)]
        public string? Rol { get; set; } // "admin" o "empleado"

        [Column("contraseña")]
        [StringLength(100)]
        public string? Contraseña { get; set; } // Asegúrate de tener esta columna en la base de datos
    }
}
