using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Rios.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public int IdInmuebles { get; set; }
        public int IdClientes { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaFinAnticipada { get; set; }
        public decimal MontoMensual { get; set; }
        public decimal? Multa { get; set; }
        public required string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
