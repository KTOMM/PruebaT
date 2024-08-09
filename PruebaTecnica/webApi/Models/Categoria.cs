using System;
using System.Collections.Generic;

namespace webApi.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int Idcategoria { get; set; }
        public string? Categoria1 { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
