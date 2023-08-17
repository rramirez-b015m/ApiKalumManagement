using System.ComponentModel.DataAnnotations;

namespace KalumManagement.Entities
{
    public class CarreraTecnica
    {
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        public string CarreraId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(128, MinimumLength = 5, ErrorMessage = "La cantidad minima de caracteres es {2} y el maximo es {1}")]
        public string Nombre { get; set; }
        public virtual ICollection<Aspirante> Aspirantes { get; set; }

    }
}