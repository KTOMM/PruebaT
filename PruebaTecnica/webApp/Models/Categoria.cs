using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webApp.Models
{
    public class Categoria
    {

        public int Idcategoria { get; set; }
        [DisplayName("Categoria")]
        [Required(ErrorMessage ="Ingrese una categoria.")]
        public string? Categoria1 { get; set; }
    }
}
