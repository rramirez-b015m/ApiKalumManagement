using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KalumManagement.Entities;

namespace KalumManagement.Dtos
{
    public class AspiranteDTO
    {
        public string NoExpediente {get;set;}
        public string Nombrecompleto{get; set;}       
        public string Direccion {get;set;}
        public string Telefono {get;set;}
        public string Email {get; set;}
        public string Estatus {get; set;}
        public virtual ExamenAdmisionCreateDTO ExamenAdmision {get; set;}
        public virtual JornadaCreateDTO Jornada {get; set;}
        public virtual CarreraTecnicaCreateDTO CarreraTecnica {get; set;}

    }
}