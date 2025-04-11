using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Rios.Models
{
    [Table("imagenes")] // Cambiado de "imagenes_inmuebles" a "imagenes"
    public class ImagenInmueble
    {
        [Key]
        [Column("idimagen")]
        public int IdImagen { get; set; } // Asegurarse de que esta propiedad exista y esté correctamente configurada

        [Required]
        [Column("idinmueble")]
        public int IdInmueble { get; set; }

        [Required]
        [Column("ruta_imagen")]
        public string Ruta { get; set; } = string.Empty; // Agregar la propiedad Ruta para coincidir con el uso en la vista

        [ForeignKey("IdInmueble")]
        public Propiedad Inmueble { get; set; } = null!;

        
    }
}
