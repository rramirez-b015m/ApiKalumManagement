using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KalumManagement.Entities;


namespace KalumManagement.Dtos
{
    public class ExamenAdmisionDTO
    {
    
        public string ExamenId{get;set;}
      
        public DateTime FechaExamen{get;set;}

        public virtual List<Aspirante> Aspirantes {get; set;}
        
    }
}