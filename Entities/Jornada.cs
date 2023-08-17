using System.ComponentModel.DataAnnotations;

namespace KalumManagement.Entities
{
    public class Jornada
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string JornadaId{get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(128, MinimumLength = 2, ErrorMessage ="La cantidad minima de caracteres es {2} y el maximo es {1}")]
        public string NombreCorto{get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Descripcion{get; set;}

        public virtual ICollection<Aspirante> Aspirantes {get; set;}
    }
}