using System;
using System.Collections.Generic;

namespace webApi.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Cotizaciones = new HashSet<Cotizacione>();
            Reservas = new HashSet<Reserva>();
        }

        public int IdProducto { get; set; }
        public string? Producto1 { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Existencia { get; set; }
        public string? FechaIng { get; set; }
        public int? Idcategoria { get; set; }

        public virtual Categoria? IdcategoriaNavigation { get; set; }
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
