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

        [Required]
        [Column("nombre")]
        public required string Nombre { get; set; }

        [Required]
        [Column("apellido")]
        public required string Apellido { get; set; }

        [Required]
        [Column("dni")]
        public required int DNI { get; set; }

        [Column("cuit")]
        public string CUIT { get; set; } = string.Empty;

        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Column("mail")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Mail { get; set; } = string.Empty;

        [Column("domicilio")]
        public string Domicilio { get; set; } = string.Empty;

        [Column("localidad")]
        public string Localidad { get; set; } = string.Empty;

        [Column("provincia")]
        public string Provincia { get; set; } = string.Empty;

        [Column("estado")]
        public bool Estado { get; set; } = true;
    }
}
