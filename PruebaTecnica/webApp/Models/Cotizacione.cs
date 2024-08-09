namespace webApp.Models
{
    public class Cotizacione
    {
        public int Idcotizaciones { get; set; }
        public string? FechaCotizacion { get; set; }
        public int? Total { get; set; }
        public string? Cliente { get; set; }
        public string? Estado { get; set; }
        public int? Idproducto { get; set; }

        public virtual Producto? IdproductoNavigation { get; set; }
    }
}
