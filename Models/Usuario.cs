using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Rios.Models
{
    [Table("usuarios")]
    public partial class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Asegura autoincremento en EF y evita el error de default value
        [Column("idusuarios")]
        public int Idusuarios { get; set; }

        [Column("nombre")]
        [StringLength(45)]
        public string? Nombre { get; set; }

        [Column("apellido")]
        [StringLength(45)]
        public string? Apellido { get; set; }

        [Column("telefono")]
        public long? Telefono { get; set; }

        [Column("mail")]
        [StringLength(45)]
        public string? Mail { get; set; }

        [Column("rol")]
        [StringLength(10)]
        public string? Rol { get; set; } // "admin" o "empleado"

        [Column("contraseña")]
        [StringLength(100)]
        public string? Contraseña { get; set; } // Asegúrate de tener esta columna en la base de datos

        [Column("estado")]
        public int? Estado { get; set; }

        // Solución 1: Permitir nulos (recomendado para imágenes opcionales)
        public string? imgperfil { get; set; }
    }

    // Método de extensión para obtener la ruta de la imagen de perfil
    public static class UsuarioExtensions
    {
        public static string GetImgPerfilUrl(this Usuario usuario)
        {
            return !string.IsNullOrEmpty(usuario.imgperfil)
                ? $"/imagenes/{usuario.imgperfil}"
                : "/imagenes/default.png"; // Cambia por la ruta de una imagen por defecto si lo deseas
        }
    }
}
