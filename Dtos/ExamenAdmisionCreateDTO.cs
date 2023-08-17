using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KalumManagement.Helpers;


namespace KalumManagement.Dtos
{
    public class ExamenAdmisionCreateDTO
    {
        public string ExamenId {get;set;}
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaExamen{get;set;}

    }
}