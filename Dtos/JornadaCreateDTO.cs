using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KalumManagement.Helpers;

namespace KalumManagement.Dtos
{
    public class JornadaCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(128, MinimumLength = 2, ErrorMessage ="La cantidad minima de caracteres es {2} y el maximo es {1}")]
        public string NombreCorto{get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(128, MinimumLength = 2, ErrorMessage ="La cantidad minima de caracteres es {2} y el maximo es {1}")]
        public string Descripcion{get; set;}
        

    }
}