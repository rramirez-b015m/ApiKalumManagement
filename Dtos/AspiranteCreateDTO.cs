using System.ComponentModel.DataAnnotations;
using KalumManagement.Helpers;

namespace KalumManagement.Dtos
{
    public class AspiranteCreateDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        public string Email { get; set; }
        public string Estatus { get; set; } = "NO ASIGNADO";
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string CarreraId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string ExamenId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string JornadaId { get; set; }



    }
}