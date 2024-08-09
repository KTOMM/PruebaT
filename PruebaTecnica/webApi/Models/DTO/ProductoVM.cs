using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace webApi.Models.DTO
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
        public string? Producto1 { get; set; }
    }
}
