using System;
using System.Collections.Generic;

namespace webApi.Models
{
    public partial class Reserva
    {
        public int Idreservas { get; set; }
        public string? FechaReserva { get; set; }
        public string? Cliente { get; set; }
        public string? FechaIni { get; set; }
        public string? FechaFin { get; set; }
        public string? Estado { get; set; }
        public int? Idbanco { get; set; }
        public int? Idproducto { get; set; }

        public virtual Banco? IdbancoNavigation { get; set; }
        public virtual Producto? IdproductoNavigation { get; set; }
    }
}
