using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace webApp.Models
{
    public class ProductoVM
    {
        public int IdProducto { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Existencia { get; set; }
        public string? FechaIng { get; set; }
        public int? Idcategoria { get; set; }
        public string Categoria { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "Ingrese un producto.")]
        public string? Producto1 { get; set; }
    }
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Existencia { get; set; }
        public string? FechaIng { get; set; }
        [DisplayName("Categoria")]
        public int? Idcategoria { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "Ingrese un producto.")]
        public string? Producto1 { get; set; }
    }
}
