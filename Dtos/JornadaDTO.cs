using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KalumManagement.Entities;

namespace KalumManagement.Dtos
{
    public class JornadaDTO
    {
        public string JornadaId{get;set;}
        public string NombreCorto{get;set;}
     
        public string Descripcion{get; set;}

        public virtual List<Aspirante> Aspirantes {get; set;}
    }
}