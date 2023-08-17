using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KalumManagement.Entities;


namespace KalumManagement.Dtos
{
    public class CarreraTecnicaDTO
    {

        public string CarreraId { get; set; }
        public string Nombre { get; set; }
        public virtual List<Aspirante> Aspirantes { get; set; }


    }
}