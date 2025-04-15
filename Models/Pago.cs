using System;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Rios.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public int NumeroPago { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; }
        public decimal Importe { get; set; }
        public required string Concepto { get; set; }
        public required string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaAnulacion { get; set; }
    }
}
