namespace Inmobiliaria_Rios.Models
{
    public class ImagenInmueble
    {
        public int IdImagen { get; set; }
        public int IdInmueble { get; set; }
        public string RutaImagen { get; set; } = string.Empty; // Inicializar con un valor predeterminado
        public Propiedad Inmueble { get; set; } = null!; // Inicializar con null-forgiving operator
    }
}
