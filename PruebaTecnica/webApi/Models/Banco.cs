using System;
using System.Collections.Generic;

namespace webApi.Models
{
    public partial class Banco
    {
        public Banco()
        {
            Reservas = new HashSet<Reserva>();
        }

        public int Idbancos { get; set; }
        public string? Banco1 { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
