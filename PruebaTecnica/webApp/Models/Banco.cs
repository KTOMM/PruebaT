using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace webApp.Models
{
    public class Banco
    {
        public int Idbancos { get; set; }
        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Ingrese una categoria.")]
        public string? Banco1 { get; set; }
    }
}
