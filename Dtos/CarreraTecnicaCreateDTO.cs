using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KalumManagement.Helpers;
using System.ComponentModel.DataAnnotations;

namespace KalumManagement.Dtos
{
    public class CarreraTecnicaCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}