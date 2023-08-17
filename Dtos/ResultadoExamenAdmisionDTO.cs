using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KalumManagement.Entities;

namespace KalumManagement.Dtos
{
    public class ResultadoExamenAdmisionDTO
    {
        public string NoExpediente {get;set;}
        public string Anio {get;set;}
        public string Descripcion {get; set;}
        public string Nota {get;set;}

        public virtual List<Aspirante> Aspirantes {get; set;}
    }
}