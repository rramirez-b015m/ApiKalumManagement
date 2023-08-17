using System.ComponentModel.DataAnnotations;

namespace KalumManagement.Entities
{
    public class ExamenAdmision
    {
        
        public string ExamenId{get;set;}
        public DateTime FechaExamen{get;set;}
        public virtual ICollection<Aspirante> Aspirantes {get; set;}
        
    }
}